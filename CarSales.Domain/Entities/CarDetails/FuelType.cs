using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.Entities.CarDetails
{
    /// <summary>
    /// FuelType class represents the type of fuel used by a car.
    /// Petrol, Diesel, Electric
    /// </summary>
    public class FuelType
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
