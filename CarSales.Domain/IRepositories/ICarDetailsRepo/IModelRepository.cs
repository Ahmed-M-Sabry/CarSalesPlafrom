using CarSales.Domain.Entities.CarDetails;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Domain.IRepositories.ICarDetailsRepo
{
    public interface IModelRepository
    {
        IQueryable<Model> GetAllAsync();
        IQueryable<Model> GetAllActiveAsync();
        IQueryable<Model> GetAllWithBrandAsync();
        IQueryable<Model> GetByBrandIdWithBrandAsync(int brandId);
        IQueryable<Model> GetAllActiveWithBrandAsync();
        Task<Model> GetByIdAsync(int id);
        Task<Model> GetByIdIncludingBrandAsync(int id);
        Task AddAsync(Model model);
        Task UpdateAsync(Model model);
        Task SoftDeleteAsync(Model model);
        Task RestoreAsync(Model model);
        Task<Model> NameIsExistAsync(string name, int brandId, CancellationToken cancellationToken);

    }
}