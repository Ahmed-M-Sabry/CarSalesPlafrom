using CarSales.Application.Common;
using CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Models;
using CarSales.Application.Features.NewCarImageFeatures.Command.Model;
using CarSales.Application.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.NewCarImageFeatures.Command.Handler
{
    internal class DeleteImageFromNewCarPostCommandHandler : IRequestHandler<DeleteImageFromNewCarPostCommand, Result<bool>>
    {
        private readonly INewCarImageServices _newCarImageServices;
        private readonly INewCarPostServices _postService;
        private readonly IUserContextService _userContextService;

        public DeleteImageFromNewCarPostCommandHandler(
            INewCarImageServices newCarImageServices,
            INewCarPostServices postService,
            IUserContextService userContextService)
        {
            _newCarImageServices = newCarImageServices;
            _postService = postService;
            _userContextService = userContextService;
        }

        public async Task<Result<bool>> Handle(DeleteImageFromNewCarPostCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Result<bool>.Failure("User must be authenticated to delete an image.", ErrorType.Unauthorized);

            // Get the image to check its OldCarPostId
            var image = await _newCarImageServices.GetByIdAsync(request.ImageId);
            if (image == null)
                return Result<bool>.Failure("Image not found.", ErrorType.NotFound);

            // Check if the user owns the post associated with the image
            var postResult = await _postService.GetByIdAsync(userId, image.NewCarPostId, cancellationToken);
            if (!postResult.IsSuccess)
                return Result<bool>.Failure("You are not authorized to delete this image.", ErrorType.Forbidden);

            // Delete the image
            await _newCarImageServices.DeleteAsync(image);

            return Result<bool>.Success(true);
        }
    }
}
