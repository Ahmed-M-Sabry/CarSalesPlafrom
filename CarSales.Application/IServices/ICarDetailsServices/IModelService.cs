using CarSales.Domain.Entities.CarDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.IServices.ICarDetailsServices
{
    public interface IModelService
    {
        Task<IEnumerable<Model>> GetAllAsync();
        Task<IEnumerable<Model>> GetAllActiveAsync();
        Task<Model?> GetByIdAsync(int id);
        Task<Model?> GetByIdWithBrandAsync(int id);
        Task<Model> CreateAsync(Model model);
        Task UpdateAsync(Model model);
        Task DeleteAsync(Model model);
        Task RestoreAsync(Model model);
    }

}
