using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories.ICarDetailsRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Repositories.CarDetailsRepo
{
    public class ModelRepository : IModelRepository
    {
        private readonly AppDbContext _context;
        public ModelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Model model)
        {
            await _context.Models.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Model>> GetAllAsync()
        {
            return await _context.Models
                .Include(m => m.Brand)
                .ToListAsync();
        }

        public async Task<IEnumerable<Model>> GetAllActiveAsync()
        {
            return await _context.Models
                .Include(m => m.Brand)
                .Where(m => !m.IsDeleted)
                .ToListAsync();
        }

        public async Task<Model?> GetByIdAsync(int id)
        {
            return await _context.Models.FindAsync(id);
        }

        public async Task<Model?> GetByIdIncludingBrandAsync(int id)
        {
            return await _context.Models
                .Include(m => m.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
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
    }

}
