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
using CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Models;
using CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Validator;
using CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Models;
using CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Validator;
using CarSales.Application.Features.NewCarImageFeatures.Command.Model;
using CarSales.Application.Features.NewCarImageFeatures.Command.Validator;
using CarSales.Application.Features.NewCarImageFeatures.Queries.Model;
using CarSales.Application.Features.NewCarImageFeatures.Queries.Validator;
using CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.SpecificServices;
using CarSales.Application.Features.PostsFeatures.OldPost.Commands.Models;
using CarSales.Application.Features.PostsFeatures.OldPost.Commands.SpecificServices;
using CarSales.Application.Features.PostsFeatures.OldPost.Commands.Validator;
using CarSales.Application.Features.UsedCarImages.Validator;
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
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
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
            services.AddScoped<IValidator<EditOldCarPostCommands>, EditOldCarPostValidator>();

            //old Image
            services.AddScoped<IValidator<GetUsedCarImagesByPostIdQuery>, GetUsedCarImagesByPostIdQueryValidator>();
            services.AddScoped<IValidator<GetImageByIdQurey>, GetImageByIdQueryValidator>();
            services.AddScoped<IValidator<DeleteImageFromOldCarPostCommand>, DeleteImageFromOldCarPostCommandValidator>();
            services.AddScoped<IValidator<AddImagesToOldCarPostCommand>, AddImagesToOldCarPostCommandValidator>();

            // new Image
            services.AddScoped<IValidator<AddImagesToNewCarPostCommand>, AddImagesToNewCarPostCommandValidator>();
            services.AddScoped<IValidator<DeleteImageFromNewCarPostCommand>, DeleteImageFromNewCarPostCommandValidator>();
            services.AddScoped<IValidator<GetNewCarImagesByPostIdQuery>, GetNewCarImagesByPostIdQueryValidator>();
            services.AddScoped<IValidator<GetNewImageByIdQurey>, GetNewImageByIdQueryValidator>();

            // Specific Srevice
            services.AddTransient<ICarPostEditServices, CarPostEditServices>();
            services.AddTransient<ICarPostCreateServices, CarPostCreateServices>();

            services.AddTransient<INewCarPostCreateServices, NewCarPostEditServices>();



            return services;
        }
    }
}
