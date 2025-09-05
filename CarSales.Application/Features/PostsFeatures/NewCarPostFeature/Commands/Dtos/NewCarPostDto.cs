using System;
using System.Collections.Generic;

namespace CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.Dtos
{
    public class NewCarPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int ModelId { get; set; }
        public string ModelName { get; set; }

        public int FuelTypeId { get; set; }
        public string FuelTypeName { get; set; }

        public int TransmissionTypeId { get; set; }
        public string TransmissionTypeName { get; set; }

        public decimal Price { get; set; }
        public decimal? DownPayment { get; set; }
        public string? InstallmentInfo { get; set; }

        public string PhoneNumber { get; set; }
        public bool IsWhatsAppAvailable { get; set; }
        public string? Address { get; set; }

        public string SellerId { get; set; }
        public string SellerName { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<string> ImageUrls { get; set; }
    }
}
