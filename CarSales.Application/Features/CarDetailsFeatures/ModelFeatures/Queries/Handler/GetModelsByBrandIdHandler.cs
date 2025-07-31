using AutoMapper;
using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Dtos;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Handler
{
    public class GetModelsByBrandIdHandler : IRequestHandler<GetModelsByBrandIdQuery, Result<IEnumerable<ModelDto>>>
    {
        private readonly IModelService _modelService;
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public GetModelsByBrandIdHandler(IModelService modelService, IBrandService brandService, IMapper mapper)
        {
            _modelService = modelService;
            _brandService = brandService;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ModelDto>>> Handle(GetModelsByBrandIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandService.GetByIdAsync(request.BrandId);
            if (brand == null)
                return Result<IEnumerable<ModelDto>>.Failure($"Brand not found with id {request.BrandId}", ErrorType.NotFound);

            if (brand.IsDeleted)
                return Result<IEnumerable<ModelDto>>.Failure($"Brand with id {request.BrandId} is deleted", ErrorType.BadRequest);

            var models = await _modelService.GetByBrandIdWithBrandAsync(request.BrandId);

            if (models == null || !models.Any())
                return Result<IEnumerable<ModelDto>>.Failure($"No models found for Brand ID {request.BrandId}", ErrorType.NotFound);

            var modelDtos = _mapper.Map<IEnumerable<ModelDto>>(models);
            return Result<IEnumerable<ModelDto>>.Success(modelDtos, "Models retrieved successfully");
        }
    }
}