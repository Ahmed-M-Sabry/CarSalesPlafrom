using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories.ICarDetailsRepo;
using CarSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Repositories.CarDetailsRepo
{
    public class ModelRepository : IModelRepository
    {
        private readonly ApplicationDbContext _context;

        public ModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Model> GetAllAsync()
        {
            return _context.Models.AsQueryable();
        }

        public IQueryable<Model> GetAllActiveAsync()
        {
            return _context.Models
                .Where(m => !m.IsDeleted)
                .AsQueryable();
        }

        public async Task<Model> GetByIdAsync(int id)
        {
            return await _context.Models.FindAsync(id);
        }

        public IQueryable<Model> GetAllWithBrandAsync()
        {
            return _context.Models
                .Include(m => m.Brand)
                .AsQueryable();
        }

        public IQueryable<Model> GetAllActiveWithBrandAsync()
        {
            return _context.Models
                .Include(m => m.Brand)
                .Where(m => !m.IsDeleted)
                .AsQueryable();
        }

        public async Task<Model> GetByIdIncludingBrandAsync(int id)
        {
            return await _context.Models
                .Include(m => m.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<Model> GetByBrandIdWithBrandAsync(int brandId)
        {
            return _context.Models
                .Include(m => m.Brand)
                .Where(m => m.BrandId == brandId && !m.IsDeleted)
                .AsQueryable();
        }

        public async Task AddAsync(Model model)
        {
            await _context.Models.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Model model)
        {
            _context.Models.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Model model)
        {
            model.IsDeleted = true;
            _context.Models.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task RestoreAsync(Model model)
        {
            model.IsDeleted = false;
            _context.Models.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Model> NameIsExistAsync(string name, int brandId, CancellationToken cancellationToken)
        {
            return await _context.Models
                .FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower() && m.BrandId == brandId, cancellationToken);
        }
    }
}