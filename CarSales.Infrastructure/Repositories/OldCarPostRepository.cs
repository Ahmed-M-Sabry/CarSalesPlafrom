using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.Entities.Posts;
using CarSales.Domain.IRepositories;
using CarSales.Domain.IRepositories.CarDetailsRepo;
using CarSales.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Repositories
{
    public class OldCarPostRepository : GenericRepositoryAsync<OldCarPost>, IOldCarPostRepository
    {
        private readonly ApplicationDbContext _context;
        public OldCarPostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
