using CarSales.Application.Common;
using CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Models;
using CarSales.Application.IServices;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Handler
{
    public class GetUsedCarImagesByPostIdQueryHandler : IRequestHandler<GetUsedCarImagesByPostIdQuery, Result<List<UsedCarImage>>>
    {
        private readonly IUsedCarImageServices _usedCarImageServices;
        private readonly IOldCarPostService _postService;
        private readonly IUserContextService _userContextService;

        public GetUsedCarImagesByPostIdQueryHandler(
            IUsedCarImageServices usedCarImageServices,
            IOldCarPostService postService,
            IUserContextService userContextService)
        {
            _usedCarImageServices = usedCarImageServices;
            _postService = postService;
            _userContextService = userContextService;
        }

        public async Task<Result<List<UsedCarImage>>> Handle(GetUsedCarImagesByPostIdQuery request, CancellationToken cancellationToken)
        {
            // Get userId for authorization
            var userId = _userContextService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Result<List<UsedCarImage>>.Failure("User must be authenticated to view images.", ErrorType.Unauthorized);

            // Check if the OldCarPost exists and user has access
            var postResult = await _postService.GetByIdAsync(request.OldCarPostId);
            if (postResult is null)
                return Result<List<UsedCarImage>>.Failure("Not Car Post Found", ErrorType.NotFound);

            // Fetch images from service
            var images = await _usedCarImageServices.GetByPostIdAsync(request.OldCarPostId);
            if (images == null || !images.Any())
                return Result<List<UsedCarImage>>.Success(new List<UsedCarImage>());

            return Result<List<UsedCarImage>>.Success(images.ToList());
        }
    }
}
