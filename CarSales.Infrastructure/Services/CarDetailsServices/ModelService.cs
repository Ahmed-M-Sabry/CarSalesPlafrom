using CarSales.Application.Comman;
using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories.ICarDetailsRepo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Services.CarDetailsServices
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;

        public ModelService(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<IEnumerable<Model>> GetAllAsync()
        {
            return await _modelRepository.GetAllAsync().ToListAsync();
        }

        public async Task<IEnumerable<Model>> GetAllActiveAsync()
        {
            return await _modelRepository.GetAllActiveAsync().ToListAsync();
        }




        public async Task<Model> GetByIdAsync(int id)
        {
            return await _modelRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Model>> GetAllWithBrandAsync()
        {
            return await _modelRepository.GetAllWithBrandAsync().ToListAsync();
        }

        public async Task<IEnumerable<Model>> GetAllActiveWithBrandAsync()
        {
            return await _modelRepository.GetAllActiveWithBrandAsync().ToListAsync();
        }

        public async Task<Model> GetByIdWithBrandAsync(int id)
        {
            return await _modelRepository.GetByIdIncludingBrandAsync(id);
        }

        public async Task<IEnumerable<Model>> GetByBrandIdWithBrandAsync(int brandId)
        {
            return await _modelRepository.GetByBrandIdWithBrandAsync(brandId).ToListAsync();
        }

        public async Task<Model> NameIsExistAsync(string name, int brandId, CancellationToken cancellationToken)
        {
            return await _modelRepository.NameIsExistAsync(name, brandId, cancellationToken);
        }

        public async Task<Model> CreateAsync(Model model)
        {
            await _modelRepository.AddAsync(model);
            return model;
        }

        public async Task UpdateAsync(Model model)
        {
            await _modelRepository.UpdateAsync(model);
        }

        public async Task DeleteAsync(Model model)
        {
            await _modelRepository.SoftDeleteAsync(model);
        }

        public async Task RestoreAsync(Model model)
        {
            await _modelRepository.RestoreAsync(model);
        }

        public async Task<PagedResult<Model>> GetAllActivePaginationAsync(string nameFilter = null, int? page = null, int? pageSize = null)
        {
            var query = _modelRepository.GetAllActiveAsync();

            if (!string.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(b => b.Name.ToLower().Contains(nameFilter.ToLower()));
            }

            var totalCount = await query.CountAsync();

            if (!page.HasValue || page <= 0 || !pageSize.HasValue || pageSize <= 0)
            {
                return new PagedResult<Model>
                {
                    Data = new List<Model>(),
                    TotalCount = totalCount,
                    PageNumber = 1,
                    PageSize = 0
                };
            }

            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            var items = await query.ToListAsync();

            return new PagedResult<Model>
            {
                Data = items,
                TotalCount = totalCount,
                PageNumber = page ?? 1,
                PageSize = pageSize ?? totalCount,
            };
        }

    }
}