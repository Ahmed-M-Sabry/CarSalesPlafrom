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
    public class GetAllTransmissionTypesHandler : IRequestHandler<GetAllTransmissionTypesQuery, Result<IEnumerable<TransmissionType>>>
    {
        private readonly ITransmissionTypeService _transmissionTypeService;

        public GetAllTransmissionTypesHandler(ITransmissionTypeService transmissionTypeService)
        {
            _transmissionTypeService = transmissionTypeService;
        }

        public async Task<Result<IEnumerable<TransmissionType>>> Handle(GetAllTransmissionTypesQuery request, CancellationToken cancellationToken)
        {
            var allTransmissionTypes = await _transmissionTypeService.GetAllAsync();

            if (allTransmissionTypes is null || !allTransmissionTypes.Any())
                return Result<IEnumerable<TransmissionType>>.Failure("No transmission types found", ErrorType.NotFound);

            return Result<IEnumerable<TransmissionType>>.Success(allTransmissionTypes, "Transmission types retrieved successfully");
        }
    }
}