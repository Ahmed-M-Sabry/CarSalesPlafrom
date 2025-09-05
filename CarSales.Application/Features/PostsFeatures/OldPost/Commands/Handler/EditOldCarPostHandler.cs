using AutoMapper;
using CarSales.Application.Common;
using CarSales.Application.Features.PostsFeatures.OldPost.Commands.Models;
using CarSales.Application.Features.PostsFeatures.OldPost.Commands.SpecificServices;
using CarSales.Application.IServices;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.Entities.Posts;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.PostsFeatures.OldPost.Commands.Handler
{
    public class EditOldCarPostHandler : IRequestHandler<EditOldCarPostCommands, Result<OldCarPost>>
    {
        private readonly ICarPostEditServices _carPostEditServices;
        private readonly IUserContextService _userContextService;
        private readonly IOldCarPostService _postService;
        private readonly IMapper _mapper;

        public EditOldCarPostHandler(
            ICarPostEditServices carPostEditServices,
            IUserContextService userContextService,
            IOldCarPostService postService,
            IMapper mapper)
        {
            _carPostEditServices = carPostEditServices;
            _userContextService = userContextService;
            _postService = postService;
            _mapper = mapper;
        }

        public async Task<Result<OldCarPost>> Handle(EditOldCarPostCommands request, CancellationToken cancellationToken)
        {

            // Step 2: Check Authentication
            var userId = _userContextService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Result<OldCarPost>.Failure("User must be authenticated.", ErrorType.Unauthorized);

            // Step 3: Domain Validation
            var validation = await _carPostEditServices.ValidateEditAsync(request, userId, cancellationToken);
            if (!validation.IsSuccess)
                return validation;


            var carPost = _mapper.Map(request, validation.Value);
            if (carPost == null)
                return Result<OldCarPost>.Failure("Can't Map", ErrorType.BadRequest);

            carPost.SellerId = userId;

            await _postService.UpdateAsync(carPost);

            return Result<OldCarPost>.Success(carPost, "Updated Success");

            //carPostExistsValue.Year = request.Year;
            //carPostExistsValue.Title = request.Title;
            //carPostExistsValue.IsTaxi = request.IsTaxi;
            //carPostExistsValue.ModelId = request.ModelId;
            //carPostExistsValue.BrandId = request.BrandId;
            //carPostExistsValue.Address = request.Address;
            //carPostExistsValue.Address = request.Address;
            //carPostExistsValue.MileageKm = request.MileageKm;
            //carPostExistsValue.FuelTypeId = request.FuelTypeId;
            //carPostExistsValue.PhoneNumber = request.PhoneNumber;
            //carPostExistsValue.Description = request.Description;
            //carPostExistsValue.IsInstallment = request.IsInstallment;
            //carPostExistsValue.IsForSpecialNeeds = request.IsForSpecialNeeds;
            //carPostExistsValue.TransmissionTypeId = request.TransmissionTypeId;
            //carPostExistsValue.IsWhatsAppAvailable = request.IsWhatsAppAvailable;

            //await _postService.UpdateAsync(carPostExistsValue);

            //return Result<OldCarPost>.Success(carPostExistsValue , "Updated Success");
        }
    }
}
