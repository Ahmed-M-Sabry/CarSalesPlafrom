using AutoMapper;
using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Dtos;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Handler
{
    public class GetModelByIdHandler : IRequestHandler<GetModelByIdQuery, Result<ModelDto>>
    {
        private readonly IModelService _modelService;
        private readonly IMapper _mapper;

        public GetModelByIdHandler(IModelService modelService, IMapper mapper)
        {
            _modelService = modelService;
            _mapper = mapper;
        }

        public async Task<Result<ModelDto>> Handle(GetModelByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _modelService.GetByIdWithBrandAsync(request.Id);

            if (model == null)
                return Result<ModelDto>.Failure($"Model not found with id {request.Id}", ErrorType.NotFound);

            if (model.IsDeleted)
                return Result<ModelDto>.Failure($"Model with id {request.Id} is deleted", ErrorType.BadRequest);

            var modelDto = _mapper.Map<ModelDto>(model);
            return Result<ModelDto>.Success(modelDto, "Model retrieved successfully");
        }
    }
}