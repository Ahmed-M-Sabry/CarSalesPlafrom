using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories;
using CarSales.Infrastructure.Data;
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
    }
}
