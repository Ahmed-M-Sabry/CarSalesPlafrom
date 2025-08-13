using CarSales.Domain.Entities.CarDetails;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.IServices
{
    public interface IUsedCarImageServices
    {
        Task<List<UsedCarImage>> GetByPostIdAsync(int oldCarPostId);
        Task<bool> ExistsByHashAsync(string hash, string sellerId);
        Task<bool> ExistsByHashAsync(string hash, int oldCarPostId, string sellerId);
        Task AddRangeAsync(ICollection<UsedCarImage> images);
        Task<UsedCarImage> GetByIdAsync(int imageId);
        Task DeleteAsync(UsedCarImage image);
    }
}
