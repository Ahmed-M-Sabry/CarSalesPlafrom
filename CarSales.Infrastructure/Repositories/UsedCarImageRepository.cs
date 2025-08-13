using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.Entities.Posts;
using CarSales.Domain.IRepositories;
using CarSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Repositories
{
    public class UsedCarImageRepository : GenericRepositoryAsync<UsedCarImage> ,  IUsedCarImageRepository
    {
        private readonly ApplicationDbContext _context;

        public UsedCarImageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<UsedCarImage>> GetByPostIdAsync(int oldCarPostId)
        {
            return await _context.Set<UsedCarImage>()
                .Where(img => img.OldCarPostId == oldCarPostId)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<bool> ExistsByHashAsync(string hash, string sellerId)
        {
            return await _context.Set<UsedCarImage>()
                .Join(_context.Set<OldCarPost>(),
                    img => img.OldCarPostId,
                    post => post.Id,
                    (img, post) => new { img, post.SellerId })
                .AnyAsync(x => x.img.Hash == hash && x.SellerId == sellerId);
        }
        public async Task<bool> ExistsByHashAsync(string hash, int oldCarPostId, string sellerId)
        {
            return await _context.Set<UsedCarImage>()
                .Where(img => img.Hash == hash && img.OldCarPostId == oldCarPostId)
                .Join(_context.Set<OldCarPost>(),
                    img => img.OldCarPostId,
                    post => post.Id,
                    (img, post) => new { img, post.SellerId })
                .AnyAsync(x => x.SellerId == sellerId);
        }

    }
}
