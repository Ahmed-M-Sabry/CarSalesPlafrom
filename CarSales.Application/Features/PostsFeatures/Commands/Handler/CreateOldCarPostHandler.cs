using AutoMapper;
using CarSales.Application.Common;
using CarSales.Application.Features.PostsFeatures.Commands.Models;
using CarSales.Application.IServices;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.Entities.Posts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.Features.PostsFeatures.Commands.Handler
{
    public class CreateOldCarPostHandler : IRequestHandler<CreateOldCarPostCommand, Result<OldCarPost>>
    {
        private readonly IOldCarPostService _postService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IUsedCarImageServices _usedCarImageServices;

        public CreateOldCarPostHandler(
            IOldCarPostService postService,
            IFileService fileService,
            IMapper mapper,
            IUserContextService userContextService,
            IUsedCarImageServices usedCarImageServices)
        {
            _postService = postService;
            _fileService = fileService;
            _mapper = mapper;
            _userContextService = userContextService;
            _usedCarImageServices = usedCarImageServices;
        }

        public async Task<Result<OldCarPost>> Handle(CreateOldCarPostCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Result<OldCarPost>.Failure("User must be authenticated to create a post.", ErrorType.Unauthorized);

            var post = _mapper.Map<OldCarPost>(request);
            post.SellerId = userId;
            post.CreatedAt = DateTime.UtcNow;

            if (request.Images != null && request.Images.Any())
            {
                post.Images = new List<UsedCarImage>();
                foreach (var image in request.Images)
                {
                    var fileHash = await _fileService.CalculateFileHashAsync(image);
                    var imageExists = await _usedCarImageServices.ExistsByHashAsync(fileHash, userId);
                    if (imageExists)
                        continue;

                    var imageUrl = await _fileService.UploadFileAsync(image, "OldCarImages", "image");
                    if (!imageUrl.IsSuccess)
                        return Result<OldCarPost>.Failure("Can't Add Image", ErrorType.BadRequest);

                    post.Images.Add(new UsedCarImage
                    {
                        Url = imageUrl.Value,
                        Hash = fileHash,
                        CreatedAt = DateTime.UtcNow
                        // OldCarPostId will be set by the database after saving the post
                    });
                }
            }

            return await _postService.CreateAsync(post, cancellationToken);
        }
    }
}

//using AutoMapper;
//using CarSales.Application.Common;
//using CarSales.Application.Features.PostsFeatures.Commands.Models;
//using CarSales.Application.Features.PostsFeatures.Commands.SpecificServices;
//using CarSales.Application.IServices;
//using CarSales.Application.IServices.CarDetailsServices;
//using CarSales.Application.IServices.ICarDetailsServices;
//using CarSales.Domain.Entities.CarDetails;
//using CarSales.Domain.Entities.Posts;
//using FluentValidation;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CarSales.Application.Features.PostsFeatures.Commands.Handler
//{
//    public class CreateOldCarPostHandler : IRequestHandler<CreateOldCarPostCommand, Result<OldCarPost>>
//    {
//        private readonly IOldCarPostService _postService;
//        private readonly IFileService _fileService;
//        private readonly IMapper _mapper;
//        private readonly IUserContextService _userContextService;
//        private readonly ICarPostCreateServices _carPostCreateSevicses;

//        public CreateOldCarPostHandler(
//            IOldCarPostService postService,
//            IFileService fileService,
//            IMapper mapper,
//            IUserContextService userContextService,
//            ICarPostCreateServices carPostCreateSevicses)
//        {
//            _postService = postService;
//            _fileService = fileService;
//            _mapper = mapper;
//            _userContextService = userContextService;
//            _carPostCreateSevicses = carPostCreateSevicses;
//        }

//        public async Task<Result<OldCarPost>> Handle(CreateOldCarPostCommand request, CancellationToken cancellationToken)
//        {
//            var userId = _userContextService.GetUserId();
//            if (string.IsNullOrEmpty(userId))
//                return Result<OldCarPost>.Failure("User must be authenticated to create a post.", ErrorType.Unauthorized);

//            var validationResult = await _carPostCreateSevicses.ValidateCreateAsync(request, cancellationToken);
//            if (!validationResult.IsSuccess)
//                return validationResult;

//            // Map the command to OldCarPost
//            var post = _mapper.Map<OldCarPost>(request);
//            post.SellerId = userId;
//            post.CreatedAt = DateTime.UtcNow;

//            // Upload images if provided
//            if (request.Images != null && request.Images.Any())
//            {
//                post.Images = new List<UsedCarImage>();
//                foreach (var image in request.Images)
//                {
//                    var imageUrl = await _fileService.UploadFileAsync(image, "OldCarImages", "image");
//                    post.Images.Add(new UsedCarImage
//                    {
//                        Url = imageUrl.Value,
//                        CreatedAt = DateTime.UtcNow
//                    });
//                }
//            }

//            // Create the post
//            return await _postService.CreateAsync(post, cancellationToken);
//        }
//    }

//}
