using CarSales.Domain.Entities.Posts;
using CarSales.Domain.IRepositories;
using CarSales.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Repositories
{
    public class NewCarPostRepository : GenericRepositoryAsync<NewCarPost> , INewCarPostRepository
    {
        private readonly ApplicationDbContext _context;
        public NewCarPostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
