using CarSales.Domain.Entities.CarDetails;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.IServices.ICarDetailsServices
{
    public interface IModelService
    {
        Task<IEnumerable<Model>> GetAllAsync();
        Task<IEnumerable<Model>> GetAllActiveAsync();
        Task<Model> GetByIdAsync(int id);
        Task<IEnumerable<Model>> GetAllWithBrandAsync();
        Task<IEnumerable<Model>> GetAllActiveWithBrandAsync();
        Task<Model> GetByIdWithBrandAsync(int id);
        Task<IEnumerable<Model>> GetByBrandIdWithBrandAsync(int brandId);
        Task<Model> NameIsExistAsync(string name, int brandId, CancellationToken cancellationToken);
        Task<Model> CreateAsync(Model model);
        Task UpdateAsync(Model model);
        Task DeleteAsync(Model model);
        Task RestoreAsync(Model model);
    }
}