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
    public class GetActiveModelsHandler : IRequestHandler<GetActiveModelsQuery, Result<IEnumerable<ModelDto>>>
    {
        private readonly IModelService _modelService;
        private readonly IMapper _mapper;

        public GetActiveModelsHandler(IModelService modelService, IMapper mapper)
        {
            _modelService = modelService;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ModelDto>>> Handle(GetActiveModelsQuery request, CancellationToken cancellationToken)
        {
            var activeModels = await _modelService.GetAllActiveWithBrandAsync();

            if (activeModels == null || !activeModels.Any())
                return Result<IEnumerable<ModelDto>>.Failure("No active models found", ErrorType.NotFound);

            var modelDtos = _mapper.Map<IEnumerable<ModelDto>>(activeModels);
            return Result<IEnumerable<ModelDto>>.Success(modelDtos, "Active models retrieved successfully");
        }
    }
}