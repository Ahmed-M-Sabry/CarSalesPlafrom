using CarSales.Application.IServices;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Services
{
    public class UsedCarImageServices : IUsedCarImageServices
    {
        private readonly IUsedCarImageRepository _usedCarImageRepository;
        public UsedCarImageServices(IUsedCarImageRepository usedCarImageRepository)
        {
            _usedCarImageRepository = usedCarImageRepository;
        }

        public async Task<List<UsedCarImage>> GetByPostIdAsync(int oldCarPostId)
        {
            return await _usedCarImageRepository.GetByPostIdAsync(oldCarPostId);
        }
        public async Task<bool> ExistsByHashAsync(string hash, string sellerId)
        {
            return await _usedCarImageRepository.ExistsByHashAsync(hash, sellerId);
        }

        public async Task<bool> ExistsByHashAsync(string hash, int oldCarPostId, string sellerId)
        {
            return await _usedCarImageRepository.ExistsByHashAsync(hash, oldCarPostId, sellerId);
        }

        public async Task AddRangeAsync(ICollection<UsedCarImage> images)
        {
            await _usedCarImageRepository.AddRangeAsync(images);
        }

        public async Task<UsedCarImage> GetByIdAsync(int imageId)
        {
            return await _usedCarImageRepository.GetByIdAsync(imageId);
        }

        public async Task DeleteAsync(UsedCarImage image)
        {
            _usedCarImageRepository.DeleteAsync(image);
        }
    }
}
