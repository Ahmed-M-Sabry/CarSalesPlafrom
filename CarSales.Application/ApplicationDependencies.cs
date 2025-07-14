using AutoMapper;
using CarSales.Application.Features.AuthenticationFeatures.Command.Model;
using CarSales.Application.Features.AuthenticationFeatures.Command.Validation;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CarSales.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {

            services.AddMediatR(c => c.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            services.AddScoped<IValidator<AddNewUserCommand>, AddNewUserValidator>();

            return services;
        }
    }
}
