using Microsoft.AspNetCore.Identity;
using MiniECommerce.Application.Abstractions.Services;
using MiniECommerce.Application.DTOs.User;
using MiniECommerce.Domain.Entities.Identity;
using E = MiniECommerce.Domain.Entities.Identity;

namespace MiniECommerce.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<E.AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                NameSurname = model.NameSurname
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanici basariyla olusturulmustur.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }
    }
}
