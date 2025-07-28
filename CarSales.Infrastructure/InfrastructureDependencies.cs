using CarSales.Application.IServices;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.IRepositories;
using CarSales.Domain.IRepositories.CarDetailsRepo;
using CarSales.Domain.IRepositories.ICarDetailsRepo;
using CarSales.Infrastructure.Data;
using CarSales.Infrastructure.Repositories.CarDetailsRepo;
using CarSales.Infrastructure.Services;
using CarSales.Infrastructure.Services.CarDetailsServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarSales.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Add your infrastructure dependencies here, e.g. DbContext, Repositories, etc.
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            // Services
            services.AddTransient<IIdentityServies, IdentityServies>();
            services.AddTransient<IModelService, ModelService>();
            services.AddTransient<ITransmissionTypeService, TransmissionTypeService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IFuelTypeService, FuelTypeService>();



            // Repositories
            services.AddTransient<IModelRepository, ModelRepository>();
            services.AddTransient<ITransmissionTypeRepository, TransmissionTypeRepository>();
            services.AddTransient<IFuelTypeRepository, FuelTypeRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();


            return services;
        }
    }
}
