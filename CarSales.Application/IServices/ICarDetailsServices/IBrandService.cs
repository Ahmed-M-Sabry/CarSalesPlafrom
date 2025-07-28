using CarSales.Domain.Entities.CarDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.IServices.CarDetailsServices
{
    public interface IBrandService
    {
        Task<Brand> GetByIdAsync(int id);
        Task<IEnumerable<Brand>> GetAllAsync(); 
        Task DeleteAsync(int id); 
        Task<Brand> CreateAsync(Brand brand);
        Task UpdateAsync(Brand brand);

            
    }
}
