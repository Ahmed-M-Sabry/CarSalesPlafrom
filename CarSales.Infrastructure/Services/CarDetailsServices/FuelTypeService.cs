using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories.CarDetailsRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Services.CarDetailsServices
{
    public class FuelTypeService : IFuelTypeService
    {
        private readonly IFuelTypeRepository _fuelTypeRepository;

        public FuelTypeService(IFuelTypeRepository fuelTypeRepository)
        {
            _fuelTypeRepository = fuelTypeRepository;
        }

        public async Task<FuelType> CreateAsync(FuelType fuelType)
        {
            await _fuelTypeRepository.AddAsync(fuelType);
            return fuelType;
        }

        public async Task<IEnumerable<FuelType>> GetAllAsync()
        {
            return await _fuelTypeRepository.GetAllActiveAsync();
        }

        public async Task<FuelType> GetByIdAsync(int id)
        {
            var fuelType = await _fuelTypeRepository.GetByIdAsync(id);
            if (fuelType == null || fuelType.IsDeleted)
                throw new Exception("FuelType not found");

            return fuelType;
        }

        public async Task UpdateAsync(FuelType fuelType)
        {
            await _fuelTypeRepository.UpdateAsync(fuelType);
        }

        public async Task DeleteAsync(int id)
        {
            var fuelType = await _fuelTypeRepository.GetByIdAsync(id);
            if (fuelType == null)
                throw new Exception("FuelType not found");

            await _fuelTypeRepository.SoftDeleteAsync(fuelType);
        }
    }

}
