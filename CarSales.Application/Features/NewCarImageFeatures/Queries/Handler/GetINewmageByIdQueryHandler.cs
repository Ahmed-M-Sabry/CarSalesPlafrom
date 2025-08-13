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
    public class GetINewmageByIdQueryHandler : IRequestHandler<GetNewImageByIdQurey, Result<NewCarImage>>
    {
        private readonly INewCarImageServices _newCarImageServices;
        private readonly INewCarPostServices _postService;
        private readonly IUserContextService _userContextService;

        public GetINewmageByIdQueryHandler(
            INewCarImageServices newCarImageServices,
            INewCarPostServices postService,
            IUserContextService userContextService)
        {
            _newCarImageServices = newCarImageServices;
            _postService = postService;
            _userContextService = userContextService;
        }

        public async Task<Result<NewCarImage>> Handle(GetNewImageByIdQurey request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Result<NewCarImage>.Failure("User must be authenticated to get an image.", ErrorType.Unauthorized);

            // Get the image
            var image = await _newCarImageServices.GetByIdAsync(request.ImageId);
            if (image == null)
                return Result<NewCarImage>.Failure("Image not found.", ErrorType.NotFound);

            // Check if the user owns the post associated with the image
            var postResult = await _postService.GetByIdAsync(userId, image.NewCarPostId, cancellationToken);
            if (!postResult.IsSuccess)
                return Result<NewCarImage>.Failure("You are not authorized to access this image.", ErrorType.Unauthorized);

            return Result<NewCarImage>.Success(image);
        }
    }
}
