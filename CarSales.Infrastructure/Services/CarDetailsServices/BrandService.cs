using CarSales.Application.Comman;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories.CarDetailsRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Services.CarDetailsServices
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<Brand> CreateAsync(Brand brand)
        {
            await _brandRepository.AddAsync(brand);
            return brand;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _brandRepository.GetAllAsync();
        }

        public async Task DeleteAsync(Brand brand)
        {
            await _brandRepository.SoftDeleteAsync(brand);
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Brand brand)
        {
            await _brandRepository.UpdateAsync(brand);
        }
        public async Task<Brand> RestoreAsync(Brand brand)
        {
            return await _brandRepository.RestoreAsync(brand);
        }


        public async Task<Brand> NameIsExistAsync(string name, CancellationToken cancellationToken)
        {
            return await _brandRepository.NameIsExistAsync(name);
        }


        public async Task<IEnumerable<Brand>> GetAllActiveAsync()
        {
            return await _brandRepository.GetAllActiveAsync().ToListAsync();
        }

        public async Task<PagedResult<Brand>> GetAllActivePaginationAsync(string nameFilter = null, int? page = null, int? pageSize = null)
        {
            var query = _brandRepository.GetAllActiveAsync();

            if (!string.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(b => b.Name.ToLower().Contains(nameFilter.ToLower()));
            }

            var totalCount = await query.CountAsync();
            if (!page.HasValue || page <= 0 || !pageSize.HasValue || pageSize <= 0)
            {
                return new PagedResult<Brand>
                {
                    Data = new List<Brand>(),
                    TotalCount = totalCount,
                    PageNumber = 1,
                    PageSize = 0
                };
            }

            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            var items = await query.ToListAsync();

            return new PagedResult<Brand>
            {
                Data = items,
                TotalCount = totalCount,
                PageNumber = page ?? 1,
                PageSize = pageSize ?? totalCount,
            };
        }


    }
}
