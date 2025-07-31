using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories.ICarDetailsRepo;
using CarSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Repositories.CarDetailsRepo
{
    public class TransmissionTypeRepository : GenericRepositoryAsync<TransmissionType>, ITransmissionTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public TransmissionTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransmissionType>> GetAllActiveAsync()
        {
            return await _context.TransmissionTypes
                .Where(t => !t.IsDeleted)
                .ToListAsync();
        }

        public async Task SoftDeleteAsync(TransmissionType transmissionType)
        {
            transmissionType.IsDeleted = true;
            _context.TransmissionTypes.Update(transmissionType);
            await _context.SaveChangesAsync();
        }
        public async Task<TransmissionType> RestoreAsync(TransmissionType item)
        {
            item.IsDeleted = false;
            _context.TransmissionTypes.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public  async Task<TransmissionType> NameIsExistAsync(string name, CancellationToken cancellationToken)
        {
            return await _context.TransmissionTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        }
    }

}
