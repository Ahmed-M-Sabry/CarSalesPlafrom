//using CarSales.Domain.Entities.Posts;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;

//namespace CarSales.Domain.Entities.CarDetails
//{
//    public class UsedCarImage
//    {
//        public int Id { get; set; }

//        public string Url { get; set; }

//        public DateTime CreatedAt { get; set; }

//        public int OldCarPostId { get; set; }
//        [JsonIgnore]
//        public OldCarPost OldCarPost { get; set; }
//    }
//}

////---------------
///
using CarSales.Domain.Entities.Posts;
using System;
using System.Text.Json.Serialization;

namespace CarSales.Domain.Entities.CarDetails
{
    public class UsedCarImage
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string? Hash { get; set; } // Added for duplicate checking

        public DateTime CreatedAt { get; set; }

        public int OldCarPostId { get; set; }
        [JsonIgnore]
        public OldCarPost OldCarPost { get; set; }
    }
}