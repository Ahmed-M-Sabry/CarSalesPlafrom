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
    public class FuelTypeRepository : GenericRepositoryAsync<FuelType>, IFuelTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public FuelTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FuelType>> GetAllActiveAsync()
        {
            return await _context.FuelTypes
                .Where(f => !f.IsDeleted)
                .ToListAsync();
        }

        public async Task SoftDeleteAsync(FuelType fuelType)
        {
            fuelType.IsDeleted = true;
            _context.FuelTypes.Update(fuelType);
            await _context.SaveChangesAsync();
        }
    }

}
