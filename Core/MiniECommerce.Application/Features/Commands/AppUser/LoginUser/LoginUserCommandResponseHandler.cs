using MediatR;
using Microsoft.AspNetCore.Identity;
using MiniECommerce.Application.Abstractions.Services;
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

        readonly IAuthService _authService;

        public LoginUserCommandResponseHandler(
            UserManager<U.AppUser> userManager,
            SignInManager<U.AppUser> singInManager,
            ITokenHandler tokenHandler,
            IAuthService authService)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _tokenHandler = tokenHandler;
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginAsync(request.UserNameOrEmail, request.Password, 15);
            return new LoginUserSuccessCommandResponse()
            {
                Token = token
            };
        }
    }
}
