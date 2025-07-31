using AutoMapper;
using CarSales.Application.Features.AuthenticationFeatures.LoginUser.Command.Model;
using CarSales.Application.Features.AuthenticationFeatures.LoginUser.Command.Validator;
using CarSales.Application.Features.AuthenticationFeatures.RegisterUser.Command.Model;
using CarSales.Application.Features.AuthenticationFeatures.RegisterUser.Command.Validation;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Validator;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Validator;
using CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Validator;
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
            services.AddScoped<IValidator<UserLogInCommand>, UserLogInValidator>();

            // Brand
            services.AddScoped<IValidator<CreateBrandCommand>, CreateBrandValidator>();
            services.AddScoped<IValidator<EditBrandCommand>, EditBrandValidator>();

            // EditFuelType
            services.AddScoped<IValidator<CreateFuelTypeCommand>, CreateFuelTypeValidator>();
            services.AddScoped<IValidator<EditFuelTypeCommand>, EditFuelTypeValidator>();

            // TransmissionType
            services.AddScoped<IValidator<CreateTransmissionTypeCommand>, CreateTransmissionTypeValidator>();
            services.AddScoped<IValidator<EditTransmissionTypeCommand>, EditTransmissionTypeValidator>();

            return services;
        }
    }
}
