using AutoMapper;
using CarSales.Application.Common;
using CarSales.Application.Features.PostsFeatures.Commands.Models;
using CarSales.Application.IServices;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.Entities.Posts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.PostsFeatures.Commands.Handler
{
    public class CreateOldCarPostHandler : IRequestHandler<CreateOldCarPostCommand, Result<OldCarPost>>
    {
        private readonly IOldCarPostService _postService;
        private readonly IFileService _fileService;
        private readonly IIdentityServies _identityServices;
        private readonly IValidator<CreateOldCarPostCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IBrandService _brandService;
        private readonly IModelService _modelService;
        private readonly IFuelTypeService _fuelTypeService;

        public CreateOldCarPostHandler(
            IOldCarPostService postService,
            IFileService fileService,
            IIdentityServies identityServices,
            IValidator<CreateOldCarPostCommand> validator,
            IMapper mapper,
            IBrandService brandService,
            IModelService modelService,
            IUserContextService userContextService,
            IFuelTypeService fuelTypeService)
        {
            _postService = postService;
            _fileService = fileService;
            _identityServices = identityServices;
            _validator = validator;
            _mapper = mapper;
            _modelService = modelService;
            _brandService = brandService;
            _userContextService = userContextService;
            _fuelTypeService = fuelTypeService;
        }

        public async Task<Result<OldCarPost>> Handle(CreateOldCarPostCommand request, CancellationToken cancellationToken)
        {
            // Validate the request
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<OldCarPost>.Failure(string.Join(" | ", errors), ErrorType.BadRequest);
            }

            var userId = _userContextService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Result<OldCarPost>.Failure("User must be authenticated to create a post.", ErrorType.Unauthorized);

            var user = await _identityServices.IsUserExist(userId);
            if (user == null)
                return Result<OldCarPost>.Failure("User not found.", ErrorType.Unauthorized);

            // Validate Brand, Model, FuelType, and TransmissionType
            var brandExists = await _brandService.GetByIdAsync(request.BrandId);
            if (brandExists is null)
                return Result<OldCarPost>.Failure($"Brand with ID {request.BrandId} does not exist or is deleted.", ErrorType.NotFound);

            // Validate Model
            var modelExists = await _modelService.GetByIdAsync(request.ModelId);
            if (modelExists is null)
                return Result<OldCarPost>.Failure($"Model with ID {request.ModelId} does not exist or is deleted.", ErrorType.NotFound);

            // Validate Model belongs to the brand
            if(modelExists.BrandId != request.BrandId)
                return Result<OldCarPost>.Failure($"Model with ID {request.ModelId} does not belong to the brand with ID {request.BrandId}.", ErrorType.BadRequest);

            // Validate FuelTpe
            var fuelTypeExists = await _fuelTypeService.GetByIdAsync(request.FuelTypeId);
            if (fuelTypeExists is null)
                return Result<OldCarPost>.Failure($"FuelType with ID {request.FuelTypeId} does not exist or is deleted.", ErrorType.NotFound);

            // Map the command to OldCarPost
            var post = _mapper.Map<OldCarPost>(request);
            post.SellerId = userId;
            post.CreatedAt = DateTime.UtcNow;

            // Upload images if provided
            if (request.Images != null && request.Images.Any())
            {
                post.Images = new List<UsedCarImage>();
                foreach (var image in request.Images)
                {
                    var imageUrl = await _fileService.UploadFileAsync(image, "OldCarImages", "image");
                    post.Images.Add(new UsedCarImage
                    {
                        Url = imageUrl.Value,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            // Create the post
            var result = await _postService.CreateAsync(post, cancellationToken);
            return result;
        }
    }
}
