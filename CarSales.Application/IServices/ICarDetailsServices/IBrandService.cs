using CarSales.Application.Comman;
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
        Task<IEnumerable<Brand>> GetAllAsync(); 
        Task<IEnumerable<Brand>> GetAllActiveAsync();
        Task<Brand> GetByIdAsync(int id);
        Task<Brand> CreateAsync(Brand brand);
        Task UpdateAsync(Brand brand);
        Task DeleteAsync(Brand brand); 
        Task<Brand> RestoreAsync(Brand brand);
        Task<Brand> NameIsExistAsync(string name, CancellationToken cancellationToken);
        Task<PagedResult<Brand>> GetAllActivePaginationAsync(string nameFilter = null, int? page = null, int? pageSize = null);

    }
}
