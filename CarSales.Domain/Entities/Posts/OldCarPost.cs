using CarSales.Domain.Entities.CarDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.Entities.Posts
{
    public class OldCarPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }

        public int FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }

        public int TransmissionTypeId { get; set; }
        public TransmissionType TransmissionType { get; set; }


        public int Year { get; set; }
        public int MileageKm { get; set; }
        public decimal Price { get; set; }

        public bool IsInstallment { get; set; }
        public bool IsTaxi { get; set; }
        public bool IsForSpecialNeeds { get; set; }

        public string PhoneNumber { get; set; }
        public bool IsWhatsAppAvailable { get; set; }
        public string? Address { get; set; }

        public string SellerId { get; set; }
        public ApplicationUser Seller { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<UsedCarImage> Images { get; set; }
    }
}
