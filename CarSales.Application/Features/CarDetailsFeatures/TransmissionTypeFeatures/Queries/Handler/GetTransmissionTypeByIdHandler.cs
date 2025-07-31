using AutoMapper;
using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Queries.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Queries.Handler
{
    public class GetTransmissionTypeByIdHandler : IRequestHandler<GetTransmissionTypeByIdQuery, Result<TransmissionType>>
    {
        private readonly ITransmissionTypeService _transmissionTypeService;

        public GetTransmissionTypeByIdHandler(ITransmissionTypeService transmissionTypeService)
        {
            _transmissionTypeService = transmissionTypeService;
        }

        public async Task<Result<TransmissionType>> Handle(GetTransmissionTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var transmissionType = await _transmissionTypeService.GetByIdAsync(request.Id);

            if (transmissionType is null)
                return Result<TransmissionType>.Failure($"Transmission type not found with id {request.Id}", ErrorType.NotFound);

            if (transmissionType.IsDeleted)
                return Result<TransmissionType>.Failure($"Transmission type with id {request.Id} is already deleted", ErrorType.BadRequest);

            return Result<TransmissionType>.Success(transmissionType, "Transmission type retrieved successfully");
        }
    }
}