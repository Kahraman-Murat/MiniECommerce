using MiniECommerce.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U = MiniECommerce.Domain.Entities.Identity;

namespace MiniECommerce.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshToken(string refreshToken, U.AppUser user, DateTime accessTokenDate, int AddOnAccesshTokenDate);
    }
}
