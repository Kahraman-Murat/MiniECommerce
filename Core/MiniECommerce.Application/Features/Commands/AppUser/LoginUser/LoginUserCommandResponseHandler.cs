using MediatR;
using Microsoft.AspNetCore.Identity;
using MiniECommerce.Application.Abstractions.Token;
using MiniECommerce.Application.DTOs;
using MiniECommerce.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U = MiniECommerce.Domain.Entities.Identity;

namespace MiniECommerce.Application.Features.Commands.AppUser.LoginUser
{
    internal class LoginUserCommandResponseHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<U.AppUser> _userManager;
        readonly SignInManager<U.AppUser> _singInManager;
        readonly ITokenHandler _tokenHandler;

        public LoginUserCommandResponseHandler(
            UserManager<U.AppUser> userManager, 
            SignInManager<U.AppUser> singInManager,
            ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            U.AppUser user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);

            if (user == null)
                throw new AuthenticationErrorException(); //NotFoundUserException("Kullanici veya sifre hatali...");

            SignInResult result = await _singInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded) // Authentication basarili
            {
                Token token = _tokenHandler.CreateAccessToken(5);
                return new LoginUserSuccessCommandResponse()
                {
                    Token = token
                };
            }
            //return new LoginUserErrorCommandResponse()
            //{
            //    Message = "Kullanici adi veya sifre Hatasi"
            //};
            throw new AuthenticationErrorException();


        }
    }
}
