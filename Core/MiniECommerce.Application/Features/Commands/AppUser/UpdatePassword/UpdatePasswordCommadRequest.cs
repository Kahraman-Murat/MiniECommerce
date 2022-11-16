using MediatR;

namespace MiniECommerce.Application.Features.Commands.AppUser.UpdatePassword
{
    public class UpdatePasswordCommadRequest : IRequest<UpdatePasswordCommadResponse>
    {
        public string UserId { get; set; }
        public string ResetToken { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}