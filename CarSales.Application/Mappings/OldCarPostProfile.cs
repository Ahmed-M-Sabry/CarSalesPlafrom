using AutoMapper;
using CarSales.Application.Features.PostsFeatures.Commands.Dots;
using CarSales.Application.Features.PostsFeatures.Commands.Models;
using CarSales.Domain.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Mappings
{
    internal class OldCarPostProfile : Profile
    {
        public OldCarPostProfile()
        {
            CreateMap<CreateOldCarPostCommand, OldCarPost>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.SellerId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        }
    }
}
