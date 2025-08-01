using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories.CarDetailsRepo;
using CarSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Repositories.CarDetailsRepo
{
    public class BrandRepository : GenericRepositoryAsync<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext _context;
        public BrandRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task SoftDeleteAsync(Brand brand)
        {
            brand.IsDeleted = true;
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Brand> GetAllActiveAsync()
        {
            return _context.Brands
                .Where(b => !b.IsDeleted);
        }
        public async Task<Brand> RestoreAsync(Brand brand)
        {
            brand.IsDeleted = false;
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<Brand> NameIsExistAsync(string name)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.Name.ToLower() == name.ToLower());

            return brand;
        }
    }

}
