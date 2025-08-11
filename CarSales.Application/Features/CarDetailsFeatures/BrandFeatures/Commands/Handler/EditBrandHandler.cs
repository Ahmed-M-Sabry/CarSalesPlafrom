using AutoMapper;
using CarSales.Application.Common;
using CarSales.Application.Features.AuthenticationFeatures.RegisterUser.Command.Dtos;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Handler
{
    public class EditBrandHandler : IRequestHandler<EditBrandCommand,Result<Brand>>
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public EditBrandHandler(IBrandService brandService
            , IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        public async Task<Result<Brand>> Handle(EditBrandCommand request, CancellationToken cancellationToken)
        {

            // IsExist Brand
            var isExits = await _brandService.GetByIdAsync(request.Id);
            if(isExits is null)
                return Result<Brand>.Failure($"Brand not found with id {request.Id}", ErrorType.NotFound);

            var nameIsExist = await _brandService.NameIsExistAsync(request.Name, cancellationToken);
            if (nameIsExist is not null && nameIsExist.Id != request.Id)
            {
                return Result<Brand>.Failure($"Brand with name {request.Name} already exists || Deleted : {nameIsExist.IsDeleted} With ID {nameIsExist.Id}.", ErrorType.BadRequest);
            }

            if (isExits.IsDeleted)
                return Result<Brand>.Failure($"{request.Id} Is Deleted" , ErrorType.BadRequest);

            isExits.Name = request.Name;

            await _brandService.UpdateAsync(isExits);
            return Result<Brand>.Success(isExits , "Brand Edit Success"); 
        }
    }
}
