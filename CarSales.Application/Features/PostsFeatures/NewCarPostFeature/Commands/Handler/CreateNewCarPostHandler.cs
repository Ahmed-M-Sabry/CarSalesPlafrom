using AutoMapper;
using CarSales.Application.Common;
using CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.Dtos;
using CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.Models;
using CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.SpecificServices;
using CarSales.Application.Features.PostsFeatures.OldPost.Commands.SpecificServices;
using CarSales.Application.IServices;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.Entities.Posts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.Handler
{
    public class CreateNewCarPostHandler : IRequestHandler<CreateNewCarPostCommand, Result<NewCarPost>>
    {
        private readonly INewCarPostServices _postService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly INewCarImageServices _newCarImageServices;
        private readonly INewCarPostCreateServices _carPostCreateSevicses;

        public CreateNewCarPostHandler(
            INewCarPostServices postService,
            IFileService fileService,
            IMapper mapper,
            IUserContextService userContextService,
            INewCarImageServices newCarImageServices,
            INewCarPostCreateServices carPostCreateServices)
        {
            _postService = postService;
            _fileService = fileService;
            _mapper = mapper;
            _userContextService = userContextService;
            _newCarImageServices = newCarImageServices;
            _carPostCreateSevicses = carPostCreateServices;
        }

        public async Task<Result<NewCarPost>> Handle(CreateNewCarPostCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Result<NewCarPost>.Failure("User must be authenticated to create a post.", ErrorType.Unauthorized);

            var validationResult = await _carPostCreateSevicses.ValidateCreateAsync(request, cancellationToken);
            if (!validationResult.IsSuccess)
                return validationResult;

            var post = _mapper.Map<NewCarPost>(request);
            post.SellerId = userId;
            post.CreatedAt = DateTime.UtcNow;

            if (request.Images != null && request.Images.Any())
            {
                post.Images = new List<NewCarImage>();
                foreach (var image in request.Images)
                {
                    var fileHash = await _fileService.CalculateFileHashAsync(image);
                    var imageExists = await _newCarImageServices.ExistsByHashAsync(fileHash, userId);
                    if (imageExists)
                        continue;

                    var imageUrl = await _fileService.UploadFileAsync(image, "NewCarImages", "image");
                    if (!imageUrl.IsSuccess)
                        return Result<NewCarPost>.Failure("Can't Add Image", ErrorType.BadRequest);

                    post.Images.Add(new NewCarImage
                    {
                        Url = imageUrl.Value,
                        Hash = fileHash,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            return await _postService.CreateAsync(post, cancellationToken);
        }
    }
}
