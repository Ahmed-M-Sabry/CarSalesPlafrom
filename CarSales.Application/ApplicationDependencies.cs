using AutoMapper;
using CarSales.Application.Features.AuthenticationFeatures.LoginUser.Command.Model;
using CarSales.Application.Features.AuthenticationFeatures.LoginUser.Command.Validator;
using CarSales.Application.Features.AuthenticationFeatures.RegisterUser.Command.Model;
using CarSales.Application.Features.AuthenticationFeatures.RegisterUser.Command.Validation;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Validator;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Validator;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Validator;
using CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Validator;
using CarSales.Application.Features.PostsFeatures.Commands.Models;
using CarSales.Application.Features.PostsFeatures.Validator;
using CarSales.Application.PipelineBehaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CarSales.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {

            //services.AddMediatR(c => c.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AdminAuthorizationBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UserAuthorizationBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PublicAuthorizationBehavior<,>));
            });

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


            // model
            services.AddScoped<IValidator<CreateModelCommand>, CreateModelValidator>();
            services.AddScoped<IValidator<EditModelCommand>, EditModelValidator>();

            services.AddScoped<IValidator<CreateOldCarPostCommand>, CreateOldCarPostValidator>();


            return services;
        }
    }
}
