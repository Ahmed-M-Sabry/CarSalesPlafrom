using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.IRepositories
{
    public interface IOldCarPostRepository : IGenericRepositoryAsync<OldCarPost>
    {
        Task<OldCarPost> GetByIdAsync(string userId, int id);
    }
}
