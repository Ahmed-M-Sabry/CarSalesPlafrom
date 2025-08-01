using CarSales.Application.Comman;
using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Queries.Models;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Queries.Models;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Models;
using CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Queries.Models;
using CarSales.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.PipelineBehaviors
{
    public class AdminAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminAuthorizationBehavior(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // List of commands that require Admin role
            var adminRequiredCommands = new[]
            {
                // Create
                typeof(CreateBrandCommand),
                typeof(CreateModelCommand),
                typeof(CreateFuelTypeCommand),
                typeof(CreateTransmissionTypeCommand),

                // Edit
                typeof(EditBrandCommand),
                typeof(EditModelCommand),
                typeof(EditFuelTypeCommand),
                typeof(EditTransmissionTypeCommand),

                // Delete
                typeof(DeleteBrandCommand),
                typeof(DeleteModelCommand),
                typeof(DeleteFuelTypeCommand),
                typeof(DeleteTransmissionTypeCommand),

                // Restore
                typeof(RestoreBrandCommand),
                typeof(RestoreModelCommand),
                typeof(RestoreFuelTypeCommand),
                typeof(RestoreTransmissionTypeCommand),
                
                // Get All
                typeof(GetAllBrandsQuery),
                typeof(GetAllModelsQuery),
                typeof(GetAllFuelTypesQuery),
                typeof(GetAllTransmissionTypesQuery),

            };

            // Check if the request is one of the admin-required commands
            if (adminRequiredCommands.Contains(request.GetType()))
            {
                // If there's an HTTP context (i.e., request comes from Controller)
                if (_httpContextAccessor.HttpContext != null)
                {
                    var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value;
                    if (string.IsNullOrEmpty(userId))
                        return Result<TResponse>.Failure("User ID not found in token.", ErrorType.Unauthorized) as TResponse;

                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                        return Result<TResponse>.Failure("User not found.", ErrorType.Unauthorized) as TResponse;

                    if (!await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                        return Result<TResponse>.Failure("You must be an Admin to perform this action.", ErrorType.Unauthorized) as TResponse;
                }
                else
                {
                    // If no HTTP context (e.g., called from internal service), fail by default
                    return Result<TResponse>.Failure("No user context available.", ErrorType.Unauthorized) as TResponse;
                }
            }

            // Proceed to the next handler
            return await next();
        }
    }
}