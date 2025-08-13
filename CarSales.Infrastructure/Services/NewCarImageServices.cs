using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Services
{
    public class NewCarImageServices : INewCarImageServices
    {
        private readonly INewCarImageRepository _newCarImageRepository;
        public NewCarImageServices(INewCarImageRepository newCarImageRepository)
        {
            _newCarImageRepository = newCarImageRepository;
        }
    }
}
