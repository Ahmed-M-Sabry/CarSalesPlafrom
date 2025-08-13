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
    public class NewCarImageRepository : GenericRepositoryAsync<NewCarImage> , INewCarImageRepository
    {
        private readonly ApplicationDbContext _context;
        public NewCarImageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<NewCarImage>> GetByPostIdAsync(int newCarPostId)
        {
            return await _context.Set<NewCarImage>()
                .Where(img => img.NewCarPostId == newCarPostId)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<bool> ExistsByHashAsync(string hash, string sellerId)
        {
            return await _context.Set<NewCarImage>()
                .Join(_context.Set<NewCarPost>(),
                    img => img.NewCarPostId,
                    post => post.Id,
                    (img, post) => new { img, post.SellerId })
                .AnyAsync(x => x.img.Hash == hash && x.SellerId == sellerId);
        }
        public async Task<bool> ExistsByHashAsync(string hash, int newCarPostId, string sellerId)
        {
            return await _context.Set<NewCarImage>()
                .Where(img => img.Hash == hash && img.NewCarPostId == newCarPostId)
                .Join(_context.Set<NewCarPost>(),
                    img => img.NewCarPostId,
                    post => post.Id,
                    (img, post) => new { img, post.SellerId })
                .AnyAsync(x => x.SellerId == sellerId);
        }
    }
}
