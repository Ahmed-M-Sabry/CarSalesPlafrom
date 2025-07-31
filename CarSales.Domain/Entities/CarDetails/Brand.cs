using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarSales.Domain.Entities.CarDetails
{
    /// <summary>
    /// Toyota, Hyundai
    /// </summary>
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;

        [JsonIgnore]
        public ICollection<Model> Models { get; set; }
    }
}
