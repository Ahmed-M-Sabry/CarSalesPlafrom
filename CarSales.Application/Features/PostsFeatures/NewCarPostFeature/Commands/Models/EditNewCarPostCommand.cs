using CarSales.Application.Common;
using CarSales.Domain.Entities.Posts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.Models
{
    public class EditNewCarPostCommand : IRequest<Result<NewCarPost>>
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int BrandId { get; set; }

        public int ModelId { get; set; }

        public int FuelTypeId { get; set; }

        public int TransmissionTypeId { get; set; }

        public decimal Price { get; set; }

        public decimal? DownPayment { get; set; }

        public string? InstallmentInfo { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsWhatsAppAvailable { get; set; }

        public string? Address { get; set; }

    }
}
