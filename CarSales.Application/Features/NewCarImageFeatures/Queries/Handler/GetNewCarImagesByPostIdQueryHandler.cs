using CarSales.Application.Common;
using CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Models;
using CarSales.Application.Features.NewCarImageFeatures.Queries.Model;
using CarSales.Application.IServices;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.NewCarImageFeatures.Queries.Handler
{
    public class GetNewCarImagesByPostIdQueryHandler : IRequestHandler<GetNewCarImagesByPostIdQuery, Result<List<NewCarImage>>>
    {
        private readonly INewCarImageServices _newCarImageServices;
        private readonly INewCarPostServices _postService;
        private readonly IUserContextService _userContextService;

        public GetNewCarImagesByPostIdQueryHandler(
            INewCarImageServices newCarImageServices,
            INewCarPostServices postService,
            IUserContextService userContextService)
        {
            _newCarImageServices = newCarImageServices;
            _postService = postService;
            _userContextService = userContextService;
        }

        public async Task<Result<List<NewCarImage>>> Handle(GetNewCarImagesByPostIdQuery request, CancellationToken cancellationToken)
        {
            // Get userId for authorization
            var userId = _userContextService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Result<List<NewCarImage>>.Failure("User must be authenticated to view images.", ErrorType.Unauthorized);

            // Check if the OldCarPost exists and user has access
            var postResult = await _postService.GetByIdAsync(userId, request.NewCarPostId, cancellationToken);
            if (!postResult.IsSuccess)
                return Result<List<NewCarImage>>.Failure("Not Car Post Found", ErrorType.NotFound);

            // Fetch images from service
            var images = await _newCarImageServices.GetByPostIdAsync(request.NewCarPostId);
            if (images == null || !images.Any())
                return Result<List<NewCarImage>>.Success(new List<NewCarImage>());

            return Result<List<NewCarImage>>.Success(images.ToList());
        }
    }
}
