using CarSales.Domain.Entities.Posts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarSales.Domain.Entities.CarDetails
{
    public class NewCarImage
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string? Hash { get; set; } // Added for duplicate checking

        public DateTime CreatedAt { get; set; }


        public int NewCarPostId { get; set; }
        [JsonIgnore]
        public NewCarPost NewCarPost { get; set; }
    }
}
