using CarSales.Domain.Entities.Posts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.Entities.CarDetails
{
    public class NewCarImage
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public DateTime CreatedAt { get; set; }

        public int NewCarPostId { get; set; }
        public NewCarPost NewCarPost { get; set; }
    }
}
