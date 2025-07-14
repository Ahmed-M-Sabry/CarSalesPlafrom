using CarSales.Domain.AuthenticationHepler;
using CarSales.Domain.Entities.Posts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<NewCarPost> NewCarPosts { get; set; }
        public ICollection<OldCarPost> OldCarPosts { get; set; }

        public List<RefreshToken>? refreshTokens { get; set; }

    }
}
