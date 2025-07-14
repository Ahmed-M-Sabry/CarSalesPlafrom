using AutoMapper;
using CarSales.Application.Exceptions;
using CarSales.Application.Features.AuthenticationFeatures.Command.Model;
using CarSales.Application.IServices;
using CarSales.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace CarSales.Application.Features.AuthenticationFeatures.Command.Handler
{
    public class AddNewUserHandler : IRequestHandler<AddNewUserCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityServies _identityServies;
        private readonly IValidator<AddNewUserCommand> _validator;
        private readonly IMapper _mapper;

        public AddNewUserHandler(UserManager<ApplicationUser> userManager,
            IValidator<AddNewUserCommand> validator,
            IMapper mapper
            , IIdentityServies identityServies)
        {
            _userManager = userManager;
            _validator = validator;
            _mapper = mapper;
            _identityServies = identityServies;
        }

        public async Task<string> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            // 1. Validation
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new CustomValidationException(errors);
            }

            // 2. Check Email Exists
            //var existingUser = await _userManager.FindByEmailAsync(request.Email);
            var existingUser = await _identityServies.IsEmailExist(request.Email);
            if (existingUser)
                throw new BadRequestException("Email already registered.");

            // 3. Mapping
            var newUser = _mapper.Map<ApplicationUser>(request);
            if (newUser == null)
                throw new Exception("User mapping failed.");

            // 4. Create User
            var result = await _identityServies.CreateUserAsync(newUser, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new CustomValidationException(errors);
            }

            // 5. Done
            return newUser.Id;
        }
    }

}
