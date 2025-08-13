using CarSales.Application.Common;
using CarSales.Application.Features.NewCarImageFeatures.Command.Model;
using CarSales.Application.IServices;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.NewCarImageFeatures.Command.Handler
{
    public class AddImagesToNewCarPostCommandHandler : IRequestHandler<AddImagesToNewCarPostCommand, Result<List<NewCarImage>>>
    {
        private readonly INewCarImageServices _newCarImageServices;
        private readonly INewCarPostServices _postService;
        private readonly IFileService _fileService;
        private readonly IUserContextService _userContextService;

        public AddImagesToNewCarPostCommandHandler(
            INewCarImageServices newCarImageServices,
            INewCarPostServices postService,
            IFileService fileService,
            IUserContextService userContextService)
        {
            _newCarImageServices = newCarImageServices;
            _postService = postService;
            _fileService = fileService;
            _userContextService = userContextService;
        }

        public async Task<Result<List<NewCarImage>>> Handle(AddImagesToNewCarPostCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Result<List<NewCarImage>>.Failure("User must be authenticated to add images.", ErrorType.Unauthorized);

            var postResult = await _postService.GetByIdAsync(userId, request.NewCarPostId, cancellationToken);
            if (!postResult.IsSuccess)
                return Result<List<NewCarImage>>.Failure("No Posts Found", ErrorType.NotFound);

            var newImages = new List<NewCarImage>();
            if (request.Images != null)
            {
                foreach (var image in request.Images)
                {
                    var fileHash = await _fileService.CalculateFileHashAsync(image);
                    var imageExists = await _newCarImageServices.ExistsByHashAsync(fileHash, request.NewCarPostId, userId);
                    if (imageExists)
                        continue; // Skip if image already exists in this post

                    var imageUrlResult = await _fileService.UploadFileAsync(image, "OldCarImages", "image");
                    if (!imageUrlResult.IsSuccess)
                        return Result<List<NewCarImage>>.Failure("Can't Add Image", ErrorType.BadRequest);

                    newImages.Add(new NewCarImage
                    {
                        Url = imageUrlResult.Value,
                        Hash = fileHash,
                        CreatedAt = DateTime.UtcNow,
                        NewCarPostId = request.NewCarPostId
                    });
                }
            }

            if (newImages.Any())
            {
                await _newCarImageServices.AddRangeAsync(newImages);
            }

            return Result<List<NewCarImage>>.Success(newImages);
        }
    }
}

