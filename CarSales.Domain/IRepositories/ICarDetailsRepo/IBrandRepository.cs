using CarSales.Domain.Entities.CarDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.IRepositories.CarDetailsRepo
{
    public interface IBrandRepository : IGenericRepositoryAsync<Brand>
    {
        Task SoftDeleteAsync(Brand brand);
        Task<IEnumerable<Brand>> GetAllActiveAsync();
    }
}
    