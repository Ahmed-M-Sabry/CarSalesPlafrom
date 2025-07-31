using CarSales.Application.Comman;
using CarSales.Application.Common;
using CarSales.Application.Features.AuthenticationFeatures.RegisterUser.Command.Model;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Validator;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories.CarDetailsRepo;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Handler
{
    public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, Result<Brand>>
    {
        private readonly IBrandService _brandService;
        private readonly IValidator<CreateBrandCommand> _validator;

        public CreateBrandHandler(IBrandService brandService
            , IValidator<CreateBrandCommand> validator)
        {
            _brandService = brandService;
            _validator = validator;
        }
        public async Task<Result<Brand>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var Validator = await _validator.ValidateAsync(request, cancellationToken);
            if (!Validator.IsValid)
            {
                var errors = Validator.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Brand>.Failure(string.Join(" | ", errors), ErrorType.BadRequest);
            }

            // Name is Exist or not
            var nameIsExist = await _brandService.NameIsExistAsync(request.Name, cancellationToken);
            if(nameIsExist is not null)
            {
                return Result<Brand>.Failure($"Brand with name {request.Name} already exists || Deleted : {nameIsExist.IsDeleted} With ID {nameIsExist.Id}.", ErrorType.BadRequest);
            }

            var brand = new Brand { Name = request.Name };
            var result = await _brandService.CreateAsync(brand);

            return Result<Brand>.Success(result, "Brand created successfully"); 
        }
    }
}
