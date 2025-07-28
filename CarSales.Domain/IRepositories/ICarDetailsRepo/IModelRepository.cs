using CarSales.Domain.Entities.CarDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.IRepositories.ICarDetailsRepo
{
    public interface IModelRepository
    {
        Task<IEnumerable<Model>> GetAllAsync();
        Task<IEnumerable<Model>> GetAllActiveAsync();
        Task<Model?> GetByIdAsync(int id);
        Task<Model?> GetByIdIncludingBrandAsync(int id);
        Task AddAsync(Model model);
        Task UpdateAsync(Model model);
        Task SoftDeleteAsync(Model model);
        Task RestoreAsync(Model model);
    }

}
