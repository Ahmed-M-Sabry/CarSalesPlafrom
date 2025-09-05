using AutoMapper;
using CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.Models;
using CarSales.Domain.Entities.Posts;
using System;

namespace CarSales.Application.Mappings
{
    public class NewCarPostProfile : Profile
    {
        public NewCarPostProfile()
        {
            CreateMap<CreateNewCarPostCommand, NewCarPost>()
                .ForMember(dest => dest.Images, opt => opt.Ignore()) 
                .ForMember(dest => dest.SellerId, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<EditNewCarPostCommand, NewCarPost>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
