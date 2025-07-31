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
    public class GetActiveTransmissionTypesHandler : IRequestHandler<GetActiveTransmissionTypesQuery, Result<IEnumerable<TransmissionType>>>
    {
        private readonly ITransmissionTypeService _transmissionTypeService;

        public GetActiveTransmissionTypesHandler(ITransmissionTypeService transmissionTypeService)
        {
            _transmissionTypeService = transmissionTypeService;
        }

        public async Task<Result<IEnumerable<TransmissionType>>> Handle(GetActiveTransmissionTypesQuery request, CancellationToken cancellationToken)
        {
            var activeTransmissionTypes = await _transmissionTypeService.GetAllActiveAsync();

            if (activeTransmissionTypes is null || !activeTransmissionTypes.Any())
                return Result<IEnumerable<TransmissionType>>.Failure("No active transmission types found", ErrorType.NotFound);

            return Result<IEnumerable<TransmissionType>>.Success(activeTransmissionTypes, "Active transmission types retrieved successfully");
        }
    }
}