using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Handler
{
    public class RestoreTransmissionTypeHandler : IRequestHandler<RestoreTransmissionTypeCommand, Result<bool>>
    {
        private readonly ITransmissionTypeService _transmissionTypeService;

        public RestoreTransmissionTypeHandler(ITransmissionTypeService transmissionTypeService)
        {
            _transmissionTypeService = transmissionTypeService;
        }

        public async Task<Result<bool>> Handle(RestoreTransmissionTypeCommand request, CancellationToken cancellationToken)
        {
            // Check if the transmission type exists
            var transmissionType = await _transmissionTypeService.GetByIdAsync(request.Id);
            if (transmissionType is null)
                return Result<bool>.Failure($"Transmission type not found with id {request.Id}", ErrorType.NotFound);

            // Check if transmission type is not deleted
            if (!transmissionType.IsDeleted)
                return Result<bool>.Failure($"Transmission type with id {request.Id} is not deleted", ErrorType.BadRequest);

            // Restore the transmission type
            await _transmissionTypeService.RestoreAsync(transmissionType);

            return Result<bool>.Success(true, "Transmission type restored successfully");
        }
    }
}