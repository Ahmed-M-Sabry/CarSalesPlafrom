using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Handler
{
    public class CreateTransmissionTypeHandler : IRequestHandler<CreateTransmissionTypeCommand, Result<TransmissionType>>
    {
        private readonly ITransmissionTypeService _transmissionTypeService;

        public CreateTransmissionTypeHandler(ITransmissionTypeService transmissionTypeService)
        {
            _transmissionTypeService = transmissionTypeService;
        }

        public async Task<Result<TransmissionType>> Handle(CreateTransmissionTypeCommand request, CancellationToken cancellationToken)
        {

            // Check if name exists
            var nameIsExist = await _transmissionTypeService.NameIsExistAsync(request.Name, cancellationToken);
            if (nameIsExist is not null)
            {
                return Result<TransmissionType>.Failure($"Transmission type with name {request.Name} already exists || Deleted: {nameIsExist.IsDeleted} With ID {nameIsExist.Id}.", ErrorType.BadRequest);
            }

            var transmissionType = new TransmissionType { Name = request.Name };
            var result = await _transmissionTypeService.CreateAsync(transmissionType);

            return Result<TransmissionType>.Success(result, "Transmission type created successfully");
        }
    }
}