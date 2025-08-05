using CarSales.Application.Common;
using CarSales.Application.Features.PostsFeatures.Commands.Dots;
using CarSales.Domain.Entities.Posts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.PostsFeatures.Commands.Models
{
    public class CreateOldCarPostCommand : IRequest<Result<OldCarPost>>
    {
        public string Title { get; set; }
        public string? Description { get; set; }

        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int FuelTypeId { get; set; }
        public int TransmissionTypeId { get; set; }

        public int Year { get; set; }
        public int MileageKm { get; set; }
        public decimal Price { get; set; }

        public bool IsInstallment { get; set; }
        public bool IsTaxi { get; set; }
        public bool IsForSpecialNeeds { get; set; }

        public string PhoneNumber { get; set; }
        public bool IsWhatsAppAvailable { get; set; }
        public string? Address { get; set; }

        public IFormFileCollection? Images { get; set; } 
    }

}
