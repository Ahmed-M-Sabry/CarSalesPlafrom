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
    public class GetAllModelsHandler : IRequestHandler<GetAllModelsQuery, Result<IEnumerable<ModelDto>>>
    {
        private readonly IModelService _modelService;
        private readonly IMapper _mapper;

        public GetAllModelsHandler(IModelService modelService, IMapper mapper)
        {
            _modelService = modelService;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ModelDto>>> Handle(GetAllModelsQuery request, CancellationToken cancellationToken)
        {
            var models = await _modelService.GetAllWithBrandAsync();

            if (models == null || !models.Any())
                return Result<IEnumerable<ModelDto>>.Failure("No models found", ErrorType.NotFound);

            var modelDtos = _mapper.Map<IEnumerable<ModelDto>>(models);
            return Result<IEnumerable<ModelDto>>.Success(modelDtos, "Models retrieved successfully");
        }
    }
}