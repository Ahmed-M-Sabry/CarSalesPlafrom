using CarSales.Domain.Entities.CarDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.IRepositories
{
    public interface IUsedCarImageRepository : IGenericRepositoryAsync<UsedCarImage>
    {
        Task<List<UsedCarImage>> GetByPostIdAsync(int oldCarPostId);
        Task<bool> ExistsByHashAsync(string hash, string sellerId);
        Task<bool> ExistsByHashAsync(string hash, int oldCarPostId, string sellerId);
    }
}
