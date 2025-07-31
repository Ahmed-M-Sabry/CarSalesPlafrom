using CarSales.Domain.Entities.CarDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.IRepositories.CarDetailsRepo
{
    public interface IFuelTypeRepository : IGenericRepositoryAsync<FuelType>
    {
        Task<IEnumerable<FuelType>> GetAllActiveAsync();
        Task SoftDeleteAsync(FuelType fuelType);
        Task<FuelType> RestoreAsync(FuelType fuel);
        Task<FuelType> NameIsExistAsync(string name);
    }

}
