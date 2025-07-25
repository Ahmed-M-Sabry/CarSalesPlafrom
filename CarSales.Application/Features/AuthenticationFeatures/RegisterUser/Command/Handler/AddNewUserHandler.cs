﻿using AutoMapper;
using CarSales.Application.Common;
using CarSales.Application.Features.AuthenticationFeatures.RegisterUser.Command.Dtos;
using CarSales.Application.Features.AuthenticationFeatures.RegisterUser.Command.Model;
using CarSales.Application.IServices;
using CarSales.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CarSales.Application.Features.AuthenticationFeatures.RegisterUser.Command.Handler
{
    public class AddNewUserHandler : IRequestHandler<AddNewUserCommand, Result<RegisterUserDto>>
    {
        private readonly IIdentityServies _identityServies;
        private readonly IValidator<AddNewUserCommand> _validator;
        private readonly IMapper _mapper;

        public AddNewUserHandler(
            IValidator<AddNewUserCommand> validator,
            IMapper mapper,
            IIdentityServies identityServies)
        {
            _validator = validator;
            _mapper = mapper;
            _identityServies = identityServies;
        }

        public async Task<Result<RegisterUserDto>> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            // Validation
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<RegisterUserDto>.Failure(string.Join(" | ", errors), ErrorType.BadRequest);
            }

            // Email Exists
            if (await _identityServies.IsEmailExist(request.Email, cancellationToken))
            {
                return Result<RegisterUserDto>.Failure("Email already registered.", ErrorType.Conflict);
            }

            // Map
            var newUser = _mapper.Map<ApplicationUser>(request);
            if (newUser == null)
            {
                return Result<RegisterUserDto>.Failure("User mapping failed.", ErrorType.InternalServerError);
            }

            // Create User
            var result = await _identityServies.CreateUserAsync(newUser, request.Password, cancellationToken);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result<RegisterUserDto>.Failure(string.Join(" | ", errors), ErrorType.UnprocessableEntity);
            }

            // Generate Token
            var token = await _identityServies.CreateJwtToken(newUser, cancellationToken);

            // Success
            var response = new RegisterUserDto
            {
                UserId = newUser.Id,
                Email = newUser.Email,
                Token = token,
                Role = "User"
            };

            return Result<RegisterUserDto>.Success(response);
        }
    }
}