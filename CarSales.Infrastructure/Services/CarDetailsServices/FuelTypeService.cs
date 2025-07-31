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
            return await _fuelTypeRepository.GetAllAsync();
        }

        public async Task<FuelType> GetByIdAsync(int id)
        {
            var fuelType = await _fuelTypeRepository.GetByIdAsync(id);

            return fuelType;
        }

        public async Task UpdateAsync(FuelType fuelType)
        {
            await _fuelTypeRepository.UpdateAsync(fuelType);
        }

        public async Task DeleteAsync(FuelType fuelType)
        {
            await _fuelTypeRepository.SoftDeleteAsync(fuelType);
        }
        public async Task<FuelType> RestoreAsync(FuelType fuelType)
        {
            return await _fuelTypeRepository.RestoreAsync(fuelType);
        }

        public async Task<FuelType> NameIsExistAsync(string name, CancellationToken cancellation)
        {
            return await _fuelTypeRepository.NameIsExistAsync(name);
        }

        public async Task<IEnumerable<FuelType>> GetAllActiveAsync()
        {
            return await _fuelTypeRepository.GetAllActiveAsync();
        }
    }

}
