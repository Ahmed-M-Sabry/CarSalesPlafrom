using CarSales.Domain.Entities.CarDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.IServices
{
    public interface INewCarImageServices
    {
        Task<List<NewCarImage>> GetByPostIdAsync(int newCarPostId);
        Task<bool> ExistsByHashAsync(string hash, string sellerId);
        Task<bool> ExistsByHashAsync(string hash, int newCarPostId, string sellerId);
        Task AddRangeAsync(ICollection<NewCarImage> images);
        Task<NewCarImage> GetByIdAsync(int imageId);
        Task DeleteAsync(NewCarImage image);
    }
}
