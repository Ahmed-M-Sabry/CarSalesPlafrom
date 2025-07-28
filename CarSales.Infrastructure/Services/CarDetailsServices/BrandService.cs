using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories.CarDetailsRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Services.CarDetailsServices
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<Brand> CreateAsync(Brand brand)
        {
            await _brandRepository.AddAsync(brand);
            return brand;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _brandRepository.GetAllActiveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);

            await _brandRepository.SoftDeleteAsync(brand);
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Brand brand)
        {
            await _brandRepository.UpdateAsync(brand);
        }
        public async Task<Brand> RestoreAsync(Brand brand)
        {
            return await _brandRepository.RestoreAsync(brand);
        }
    }
}
