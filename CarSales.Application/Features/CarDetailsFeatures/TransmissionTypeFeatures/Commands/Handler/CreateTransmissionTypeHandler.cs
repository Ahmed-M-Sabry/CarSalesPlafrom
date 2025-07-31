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
        private readonly IValidator<CreateTransmissionTypeCommand> _validator;

        public CreateTransmissionTypeHandler(ITransmissionTypeService transmissionTypeService, IValidator<CreateTransmissionTypeCommand> validator)
        {
            _transmissionTypeService = transmissionTypeService;
            _validator = validator;
        }

        public async Task<Result<TransmissionType>> Handle(CreateTransmissionTypeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<TransmissionType>.Failure(string.Join(" | ", errors), ErrorType.BadRequest);
            }

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