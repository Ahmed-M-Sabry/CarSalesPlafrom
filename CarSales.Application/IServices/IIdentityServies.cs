using CarSales.Domain.AuthenticationHepler;
using CarSales.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.IServices
{
    public interface IIdentityServies
    {
        Task<ResponseAuthModel> RefreshTokenAsunc(string token);
        Task<ResponseAuthModel> GenerateRefreshTokenAsync(ApplicationUser user, bool rememberMe ,CancellationToken cancellationToken = default);
        Task<bool> RevokeRefreshTokenFromCookiesAsync();
        Task<bool> IsInRole(string userId, string role);
        Task<ApplicationUser> IsUserExist(string userId);

        Task<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> IsEmailExist(string email, CancellationToken cancellationToken = default);
        Task<bool> IsPasswordExist(ApplicationUser user, string Password, CancellationToken cancellationToken = default);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password, CancellationToken cancellationToken = default);
        Task<string> CreateJwtToken(ApplicationUser user, CancellationToken cancellationToken = default);
    }
}
