using CarSales.Application.IServices;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories;
using CarSales.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Services
{
    public class NewCarImageServices : INewCarImageServices
    {
        private readonly INewCarImageRepository _newCarImageRepository;
        public NewCarImageServices(INewCarImageRepository newCarImageRepository)
        {
            _newCarImageRepository = newCarImageRepository;
        }
        public async Task<List<NewCarImage>> GetByPostIdAsync(int oldCarPostId)
        {
            return await _newCarImageRepository.GetByPostIdAsync(oldCarPostId);
        }
        public async Task<bool> ExistsByHashAsync(string hash, string sellerId)
        {
            return await _newCarImageRepository.ExistsByHashAsync(hash, sellerId);
        }

        public async Task<bool> ExistsByHashAsync(string hash, int newCarPostId, string sellerId)
        {
            return await _newCarImageRepository.ExistsByHashAsync(hash, newCarPostId, sellerId);
        }

        public async Task AddRangeAsync(ICollection<NewCarImage> images)
        {
            await _newCarImageRepository.AddRangeAsync(images);
        }

        public async Task<NewCarImage> GetByIdAsync(int imageId)
        {
            return await _newCarImageRepository.GetByIdAsync(imageId);
        }

        public async Task DeleteAsync(NewCarImage image)
        {
            _newCarImageRepository.DeleteAsync(image);
        }
    }
}
