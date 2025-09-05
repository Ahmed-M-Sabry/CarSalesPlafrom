using CarSales.Application.Common;
using CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.Models;
using CarSales.Application.Features.PostsFeatures.OldPost.Commands.Models;
using CarSales.Application.IServices;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.SpecificServices
{
    public interface INewCarPostCreateServices
    {
        Task<Result<NewCarPost>> ValidateCreateAsync(CreateNewCarPostCommand request, CancellationToken ct);

    }

    public class NewCarPostEditServices : INewCarPostCreateServices
    {
        private readonly IBrandService _brandService;
        private readonly IModelService _modelService;
        private readonly IFuelTypeService _fuelTypeService;
        private readonly ITransmissionTypeService _transmissionTypeService;

        public NewCarPostEditServices(
            IBrandService brandService,
            IModelService modelService,
            IFuelTypeService fuelTypeService,
            ITransmissionTypeService transmissionTypeService)
        {
            _brandService = brandService;
            _modelService = modelService;
            _fuelTypeService = fuelTypeService;
            _transmissionTypeService = transmissionTypeService;
        }
        public async Task<Result<NewCarPost>> ValidateCreateAsync(CreateNewCarPostCommand request, CancellationToken ct)
        {
            // Validate Brand, Model, FuelType, and TransmissionType
            var brandExists = await _brandService.GetByIdAsync(request.BrandId);
            if (brandExists is null)
                return Result<NewCarPost>.Failure($"Brand with ID {request.BrandId} does not exist or is deleted.", ErrorType.NotFound);

            // Validate Model
            var modelExists = await _modelService.GetByIdAsync(request.ModelId);
            if (modelExists is null)
                return Result<NewCarPost>.Failure($"Model with ID {request.ModelId} does not exist or is deleted.", ErrorType.NotFound);

            // Validate Model belongs to the brand
            if (modelExists.BrandId != request.BrandId)
                return Result<NewCarPost>.Failure($"Model with ID {request.ModelId} does not belong to the brand with ID {request.BrandId}.", ErrorType.BadRequest);

            // Validate FuelTpe
            var fuelTypeExists = await _fuelTypeService.GetByIdAsync(request.FuelTypeId);
            if (fuelTypeExists is null)
                return Result<NewCarPost>.Failure($"FuelType with ID {request.FuelTypeId} does not exist or is deleted.", ErrorType.NotFound);

            var transmissionTypeExists = await _transmissionTypeService.GetByIdAsync(request.TransmissionTypeId);
            if (transmissionTypeExists is null)
                return Result<NewCarPost>.Failure($"TransmissionType with ID {request.FuelTypeId} does not exist or is deleted.", ErrorType.NotFound);

            return Result<NewCarPost>.Success(new NewCarPost());
        }
    }

}
