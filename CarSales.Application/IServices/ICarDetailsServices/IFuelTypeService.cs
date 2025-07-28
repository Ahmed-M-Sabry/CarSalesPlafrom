using CarSales.Domain.Entities.CarDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.IServices.CarDetailsServices
{
    public interface IFuelTypeService
    {
        Task<FuelType> GetByIdAsync(int id);
        Task<IEnumerable<FuelType>> GetAllAsync();
        Task<FuelType> CreateAsync(FuelType fuelType);
        Task UpdateAsync(FuelType fuelType);
        Task DeleteAsync(FuelType fuelType);
        Task<FuelType> RestoreAsync(FuelType fuelType);

    }

}
