using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Handler
{
    public class DeleteTransmissionTypeHandler : IRequestHandler<DeleteTransmissionTypeCommand, Result<bool>>
    {
        private readonly ITransmissionTypeService _transmissionTypeService;

        public DeleteTransmissionTypeHandler(ITransmissionTypeService transmissionTypeService)
        {
            _transmissionTypeService = transmissionTypeService;
        }

        public async Task<Result<bool>> Handle(DeleteTransmissionTypeCommand request, CancellationToken cancellationToken)
        {
            // Check if the transmission type exists
            var transmissionType = await _transmissionTypeService.GetByIdAsync(request.Id);
            if (transmissionType is null)
                return Result<bool>.Failure($"Transmission type not found with id {request.Id}", ErrorType.NotFound);

            // Check if transmission type is already deleted
            if (transmissionType.IsDeleted)
                return Result<bool>.Failure($"Transmission type with id {request.Id} is already deleted", ErrorType.BadRequest);

            // Delete the transmission type
            await _transmissionTypeService.DeleteAsync(transmissionType);

            return Result<bool>.Success(true, "Transmission type deleted successfully");
        }
    }
}