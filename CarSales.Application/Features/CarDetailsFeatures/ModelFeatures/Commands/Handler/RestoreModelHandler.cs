using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Handler
{
    public class RestoreModelHandler : IRequestHandler<RestoreModelCommand, Result<bool>>
    {
        private readonly IModelService _modelService;
        private readonly IBrandService _brandService;

        public RestoreModelHandler(IModelService modelService, IBrandService brandService)
        {
            _modelService = modelService;
            _brandService = brandService;
        }

        public async Task<Result<bool>> Handle(RestoreModelCommand request, CancellationToken cancellationToken)
        {
            var model = await _modelService.GetByIdAsync(request.Id);
            if (model == null)
                return Result<bool>.Failure($"Model not found with id {request.Id}", ErrorType.NotFound);

            if (!model.IsDeleted)
                return Result<bool>.Failure($"Model with id {request.Id} is not deleted", ErrorType.BadRequest);

            // Check if Brand is not deleted
            var brand = await _brandService.GetByIdAsync(model.BrandId);
            if (brand == null || brand.IsDeleted)
                return Result<bool>.Failure($"Cannot restore model because its brand (ID: {model.BrandId}) is deleted or not found", ErrorType.BadRequest);

            await _modelService.RestoreAsync(model);

            return Result<bool>.Success(true, "Model restored successfully");
        }
    }
}