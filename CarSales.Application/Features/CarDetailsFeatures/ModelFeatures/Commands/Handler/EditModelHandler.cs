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
    public class EditModelHandler : IRequestHandler<EditModelCommand, Result<ModelDto>>
    {
        private readonly IModelService _modelService;
        private readonly IBrandService _brandService;
        private readonly IValidator<EditModelCommand> _validator;
        private readonly IMapper _mapper;

        public EditModelHandler(IModelService modelService, IBrandService brandService, IValidator<EditModelCommand> validator, IMapper mapper)
        {
            _modelService = modelService;
            _brandService = brandService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Result<ModelDto>> Handle(EditModelCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<ModelDto>.Failure(string.Join(" | ", errors), ErrorType.BadRequest);
            }

            // Check if Model exists
            var model = await _modelService.GetByIdAsync(request.Id);
            if (model == null)
                return Result<ModelDto>.Failure($"Model not found with id {request.Id}", ErrorType.NotFound);

            // Check if Brand exists and is not deleted
            var brand = await _brandService.GetByIdAsync(request.BrandId);
            if (brand == null)
                return Result<ModelDto>.Failure($"Brand not found with id {request.BrandId}", ErrorType.NotFound);
            if (brand.IsDeleted)
                return Result<ModelDto>.Failure($"Brand with id {request.BrandId} is deleted", ErrorType.BadRequest);

            // Check if model is deleted
            if (model.IsDeleted)
                return Result<ModelDto>.Failure($"Model with id {request.Id} is deleted", ErrorType.BadRequest);

            // Check if name exists for another model under the same brand
            var nameIsExist = await _modelService.NameIsExistAsync(request.Name, request.BrandId, cancellationToken);
            if (nameIsExist != null && nameIsExist.Id != request.Id)
            {
                return Result<ModelDto>.Failure($"Model with name {request.Name} already exists for Brand ID {request.BrandId} || Deleted: {nameIsExist.IsDeleted} With ID {nameIsExist.Id}.", ErrorType.BadRequest);
            }

            _mapper.Map(request, model);
            await _modelService.UpdateAsync(model);

            // Fetch the updated model with Brand to include BrandName in the response
            var updatedModel = await _modelService.GetByIdWithBrandAsync(model.Id);
            if (updatedModel == null)
                return Result<ModelDto>.Failure("Failed to retrieve updated model", ErrorType.InternalServerError);

            var modelDto = _mapper.Map<ModelDto>(updatedModel);
            return Result<ModelDto>.Success(modelDto, "Model updated successfully");
        }
    }
}