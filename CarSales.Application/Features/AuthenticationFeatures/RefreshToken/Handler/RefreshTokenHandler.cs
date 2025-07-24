using CarSales.Application.Common;
using CarSales.Application.Features.AuthenticationFeatures.RefreshToken.Model;
using CarSales.Application.IServices;
using CarSales.Domain.AuthenticationHepler;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.AuthenticationFeatures.RefreshToken.Handler
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, Result<ResponseAuthModel>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityServies _identityServies;
        public RefreshTokenHandler(
            IHttpContextAccessor httpContextAccessor ,
             IIdentityServies identityServies)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityServies = identityServies;
        }

        public async Task<Result<ResponseAuthModel>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["RefreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return Result<ResponseAuthModel>.Failure("No refresh token provided." , ErrorType.NotFound);

            var result = await _identityServies.RefreshTokenAsunc(refreshToken);

            if (!string.IsNullOrEmpty(result.Message))
                return Result<ResponseAuthModel>.Failure(result.Message, ErrorType.NotFound);

            return Result<ResponseAuthModel>.Success(result);


        }
    }
}
