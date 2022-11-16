using MediatR;
using MiniECommerce.Application.Abstractions.Services;
using MiniECommerce.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Commands.AppUser.UpdatePassword
{
    public class UpdatePasswordCommadHandler : IRequestHandler<UpdatePasswordCommadRequest, UpdatePasswordCommadResponse>
    {
        readonly IUserService _userService;

        public UpdatePasswordCommadHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdatePasswordCommadResponse> Handle(UpdatePasswordCommadRequest request, CancellationToken cancellationToken)
        {
            if(!request.Password.Equals(request.PasswordConfirm))
                throw new PasswordChangeFailedException("Lütfen sifreyi dogrulayiniz.");
            await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);

            return new();
        }
    }
}
