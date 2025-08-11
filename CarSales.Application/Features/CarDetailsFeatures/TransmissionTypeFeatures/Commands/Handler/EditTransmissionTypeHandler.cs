using AutoMapper;
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
    public class EditTransmissionTypeHandler : IRequestHandler<EditTransmissionTypeCommand, Result<TransmissionType>>
    {
        private readonly ITransmissionTypeService _transmissionTypeService;

        public EditTransmissionTypeHandler(ITransmissionTypeService transmissionTypeService)
        {
            _transmissionTypeService = transmissionTypeService;
        }

        public async Task<Result<TransmissionType>> Handle(EditTransmissionTypeCommand request, CancellationToken cancellationToken)
        {

            // Check if the transmission type exists
            var transmissionType = await _transmissionTypeService.GetByIdAsync(request.Id);
            if (transmissionType is null)
                return Result<TransmissionType>.Failure($"Transmission type not found with id {request.Id}", ErrorType.NotFound);

            // Check if name exists for another transmission type
            var nameIsExist = await _transmissionTypeService.NameIsExistAsync(request.Name, cancellationToken);
            if (nameIsExist is not null && nameIsExist.Id != request.Id)
            {
                return Result<TransmissionType>.Failure($"Transmission type with name {request.Name} already exists || Deleted: {nameIsExist.IsDeleted} With ID {nameIsExist.Id}.", ErrorType.BadRequest);
            }

            // Check if transmission type is deleted
            if (transmissionType.IsDeleted)
                return Result<TransmissionType>.Failure($"Transmission type with id {request.Id} is deleted", ErrorType.BadRequest);

            transmissionType.Name = request.Name;

            await _transmissionTypeService.UpdateAsync(transmissionType);
            return Result<TransmissionType>.Success(transmissionType, "Transmission type updated successfully");
        }
    }
}