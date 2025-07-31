using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Handler
{
    public class DeleteModelHandler : IRequestHandler<DeleteModelCommand, Result<bool>>
    {
        private readonly IModelService _modelService;

        public DeleteModelHandler(IModelService modelService)
        {
            _modelService = modelService;
        }

        public async Task<Result<bool>> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
        {
            var model = await _modelService.GetByIdAsync(request.Id);
            if (model == null)
                return Result<bool>.Failure($"Model not found with id {request.Id}", ErrorType.NotFound);

            if (model.IsDeleted)
                return Result<bool>.Failure($"Model with id {request.Id} is already deleted", ErrorType.BadRequest);

            await _modelService.DeleteAsync(model);

            return Result<bool>.Success(true, "Model deleted successfully");
        }
    }
}