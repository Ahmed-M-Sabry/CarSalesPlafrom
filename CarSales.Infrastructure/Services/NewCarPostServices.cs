using CarSales.Application.IServices;
using CarSales.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Services
{
    public class NewCarPostServices : INewCarPostServices
    {
        private readonly INewCarImageRepository _newCarImageRepository;
        public NewCarPostServices(INewCarImageRepository newCarImageRepository)
        {
            _newCarImageRepository = newCarImageRepository;

        }
    }
}
