using CarSales.Application.Common;
using CarSales.Application.Features.PostsFeatures.Commands.Models;
using CarSales.Application.IServices;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.PostsFeatures.Commands.SpecificServices
{
    public interface ICarPostEditServices
    {
        Task<Result<OldCarPost>> ValidateEditAsync(EditOldCarPostCommands request, string userId, CancellationToken ct);
    }

    public class CarPostEditServices : ICarPostEditServices
    {
        private readonly IOldCarPostService _postService;
        private readonly IBrandService _brandService;
        private readonly IModelService _modelService;
        private readonly IFuelTypeService _fuelTypeService;
        private readonly ITransmissionTypeService _transmissionTypeService;

        public CarPostEditServices(
            IOldCarPostService postService,
            IBrandService brandService,
            IModelService modelService,
            IFuelTypeService fuelTypeService,
            ITransmissionTypeService transmissionTypeService)
        {
            _postService = postService;
            _brandService = brandService;
            _modelService = modelService;
            _fuelTypeService = fuelTypeService;
            _transmissionTypeService = transmissionTypeService;
        }

        public async Task<Result<OldCarPost>> ValidateEditAsync(EditOldCarPostCommands request, string userId, CancellationToken ct)
        {
            var carPostExists = await _postService.GetByIdAsync(userId, request.Id, ct);
            if (!carPostExists.IsSuccess)
                return Result<OldCarPost>.Failure("You Don't have permission to Edit this post.", ErrorType.Conflict);

            var carPostExistsValue = carPostExists.Value;

            if (carPostExistsValue.BrandId != request.BrandId)
            {
                var brandExists = await _brandService.GetByIdAsync(request.BrandId);
                if (brandExists is null)
                    return Result<OldCarPost>.Failure($"Brand with ID {request.BrandId} does not exist.", ErrorType.NotFound);
            }

            if (carPostExistsValue.ModelId != request.ModelId)
            {
                var modelExists = await _modelService.GetByIdAsync(request.ModelId);
                if (modelExists is null)
                    return Result<OldCarPost>.Failure($"Model with ID {request.ModelId} does not exist.", ErrorType.NotFound);

                if (modelExists.BrandId != request.BrandId)
                    return Result<OldCarPost>.Failure($"Model does not belong to Brand {request.BrandId}.", ErrorType.BadRequest);
            }

            if (carPostExistsValue.FuelTypeId != request.FuelTypeId)
            {
                var fuelTypeExists = await _fuelTypeService.GetByIdAsync(request.FuelTypeId);
                if (fuelTypeExists is null)
                    return Result<OldCarPost>.Failure($"FuelType with ID {request.FuelTypeId} does not exist.", ErrorType.NotFound);
            }

            if (carPostExistsValue.TransmissionTypeId != request.TransmissionTypeId)
            {
                var transmissionTypeExists = await _transmissionTypeService.GetByIdAsync(request.TransmissionTypeId);
                if (transmissionTypeExists is null)
                    return Result<OldCarPost>.Failure($"TransmissionType with ID {request.TransmissionTypeId} does not exist.", ErrorType.NotFound);
            }

            return Result<OldCarPost>.Success(carPostExistsValue);
        }
    }

}
