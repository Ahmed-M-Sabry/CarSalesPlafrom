using CarSales.Application.Common;
using CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Models;
using CarSales.Application.IServices;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Handler
{
    public class AddImagesToOldCarPostCommandHandler : IRequestHandler<AddImagesToOldCarPostCommand, Result<List<UsedCarImage>>>
    {
        private readonly IUsedCarImageServices _usedCarImageServices;
        private readonly IOldCarPostService _postService;
        private readonly IFileService _fileService;
        private readonly IUserContextService _userContextService;

        public AddImagesToOldCarPostCommandHandler(
            IUsedCarImageServices usedCarImageServices,
            IOldCarPostService postService,
            IFileService fileService,
            IUserContextService userContextService)
        {
            _usedCarImageServices = usedCarImageServices;
            _postService = postService;
            _fileService = fileService;
            _userContextService = userContextService;
        }

        public async Task<Result<List<UsedCarImage>>> Handle(AddImagesToOldCarPostCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Result<List<UsedCarImage>>.Failure("User must be authenticated to add images.", ErrorType.Unauthorized);

            var postResult = await _postService.GetByIdAsync(userId, request.OldCarPostId, cancellationToken);
            if (!postResult.IsSuccess)
                return Result<List<UsedCarImage>>.Failure("No Posts Found", ErrorType.NotFound);

            var newImages = new List<UsedCarImage>();
            if (request.Images != null)
            {
                foreach (var image in request.Images)
                {
                    var fileHash = await _fileService.CalculateFileHashAsync(image);
                    var imageExists = await _usedCarImageServices.ExistsByHashAsync(fileHash, request.OldCarPostId, userId);
                    if (imageExists)
                        continue; // Skip if image already exists in this post

                    var imageUrlResult = await _fileService.UploadFileAsync(image, "OldCarImages", "image");
                    if (!imageUrlResult.IsSuccess)
                        return Result<List<UsedCarImage>>.Failure("Can't Add Image", ErrorType.BadRequest);

                    newImages.Add(new UsedCarImage
                    {
                        Url = imageUrlResult.Value,
                        Hash = fileHash,
                        CreatedAt = DateTime.UtcNow,
                        OldCarPostId = request.OldCarPostId
                    });
                }
            }

            if (newImages.Any())
            {
                await _usedCarImageServices.AddRangeAsync(newImages);
            }

            return Result<List<UsedCarImage>>.Success(newImages);
        }
    }
}
