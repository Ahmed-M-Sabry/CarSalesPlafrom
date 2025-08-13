using CarSales.Application.Common;
using CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Models;
using CarSales.Application.IServices;
using CarSales.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Handler
{
    public class DeleteImageFromOldCarPostCommandHandler : IRequestHandler<DeleteImageFromOldCarPostCommand, Result<bool>>
    {
        private readonly IUsedCarImageServices _usedCarImageServices;
        private readonly IOldCarPostService _postService;
        private readonly IUserContextService _userContextService;

        public DeleteImageFromOldCarPostCommandHandler(
            IUsedCarImageServices usedCarImageServices,
            IOldCarPostService postService,
            IUserContextService userContextService)
        {
            _usedCarImageServices = usedCarImageServices;
            _postService = postService;
            _userContextService = userContextService;
        }

        public async Task<Result<bool>> Handle(DeleteImageFromOldCarPostCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Result<bool>.Failure("User must be authenticated to delete an image.", ErrorType.Unauthorized);

            // Get the image to check its OldCarPostId
            var image = await _usedCarImageServices.GetByIdAsync(request.ImageId);
            if (image == null)
                return Result<bool>.Failure("Image not found.", ErrorType.NotFound);

            // Check if the user owns the post associated with the image
            var postResult = await _postService.GetByIdAsync(userId, image.OldCarPostId, cancellationToken);
            if (!postResult.IsSuccess)
                return Result<bool>.Failure("You are not authorized to delete this image.", ErrorType.Forbidden);

            // Delete the image
            await _usedCarImageServices.DeleteAsync(image);

            return Result<bool>.Success(true);
        }
    }
}
