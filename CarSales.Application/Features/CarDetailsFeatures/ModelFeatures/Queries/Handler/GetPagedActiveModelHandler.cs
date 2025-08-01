using CarSales.Application.Comman;
using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Handler
{
    public class GetPagedActiveModelHandler : IRequestHandler<GetPagedActiveModelQuery, Result<PagedResult<Model>>>
    {
        private readonly IModelService _modelService;
        public GetPagedActiveModelHandler(IModelService modelService)
        {
            _modelService = modelService;
        }
        public async Task<Result<PagedResult<Model>>> Handle(GetPagedActiveModelQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _modelService.GetAllActivePaginationAsync(request.name, request.PageNumber, request.PageSize);

            if (pagedResult is null || !pagedResult.Data.Any())
                return Result<PagedResult<Model>>.Failure("No Model found", ErrorType.NotFound);

            return Result<PagedResult<Model>>.Success(pagedResult, "Model retrieved successfully");

        }
    }
}
