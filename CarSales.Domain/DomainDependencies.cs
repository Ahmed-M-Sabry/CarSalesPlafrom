using CarSales.Domain.IRepositories.ICarDetailsRepo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarSales.Domain
{
    public static class DomainDependencies
    {
        public static IServiceCollection AddDomainDependencies(this IServiceCollection services)
        {



            return services;
        }
    }
}
