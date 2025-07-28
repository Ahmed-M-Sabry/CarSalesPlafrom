using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.Entities.CarDetails
{
    /// <summary>
    /// Corolla, Elantra
    /// </summary>
    public class Model
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

    }
}
