using AutoMapper;
using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Dtos;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Handler
{
    public class CreateModelHandler : IRequestHandler<CreateModelCommand, Result<ModelDto>>
    {
        private readonly IModelService _modelService;
        private readonly IBrandService _brandService;
        private readonly IValidator<CreateModelCommand> _validator;
        private readonly IMapper _mapper;

        public CreateModelHandler(IModelService modelService, IBrandService brandService, IValidator<CreateModelCommand> validator, IMapper mapper)
        {
            _modelService = modelService;
            _brandService = brandService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Result<ModelDto>> Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<ModelDto>.Failure(string.Join(" | ", errors), ErrorType.BadRequest);
            }

            // Check if Brand exists and is not deleted
            var brand = await _brandService.GetByIdAsync(request.BrandId);
            if (brand == null)
                return Result<ModelDto>.Failure($"Brand not found with id {request.BrandId}", ErrorType.NotFound);
            if (brand.IsDeleted)
                return Result<ModelDto>.Failure($"Brand with id {request.BrandId} is deleted", ErrorType.BadRequest);

            // Check if name exists for the same brand
            var nameIsExist = await _modelService.NameIsExistAsync(request.Name, request.BrandId, cancellationToken);
            if (nameIsExist != null)
            {
                return Result<ModelDto>.Failure($"Model with name {request.Name} already exists for Brand ID {request.BrandId} || Deleted: {nameIsExist.IsDeleted} With ID {nameIsExist.Id}.", ErrorType.BadRequest);
            }

            var model = _mapper.Map<Model>(request);
            var result = await _modelService.CreateAsync(model);

            //return Result<ModelDto>.Success(result, "Model created successfully");
            var createdModel = await _modelService.GetByIdWithBrandAsync(model.Id);
            if (createdModel == null)
                return Result<ModelDto>.Failure("Failed to retrieve created model", ErrorType.InternalServerError);

            var modelDto = _mapper.Map<ModelDto>(createdModel);
            return Result<ModelDto>.Success(modelDto, "Model created successfully");
        }
    }
}