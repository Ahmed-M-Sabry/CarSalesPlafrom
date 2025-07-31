using AutoMapper;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Dtos;
using CarSales.Domain.Entities.CarDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Mappings
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<CreateModelCommand, Model>();
            CreateMap<EditModelCommand, Model>();
            CreateMap<Model, ModelDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));

        }
    }
}
