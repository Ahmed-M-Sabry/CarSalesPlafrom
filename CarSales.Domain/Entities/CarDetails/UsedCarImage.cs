using CarSales.Domain.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.Entities.CarDetails
{
    public class UsedCarImage
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public DateTime CreatedAt { get; set; }

        public int OldCarPostId { get; set; }
        public OldCarPost OldCarPost { get; set; }
    }
}
