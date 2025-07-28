using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.Entities.CarDetails
{
    /// <summary>
    /// Manual, Automatic
    /// </summary>
    public class TransmissionType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
