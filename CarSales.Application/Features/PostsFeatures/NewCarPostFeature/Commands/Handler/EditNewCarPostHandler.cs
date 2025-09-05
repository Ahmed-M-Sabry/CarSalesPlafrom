using AutoMapper;
using CarSales.Application.Common;
using CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.Models;
using CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.SpecificServices;
using CarSales.Application.Features.PostsFeatures.OldPost.Commands.SpecificServices;
using CarSales.Application.IServices;
using CarSales.Domain.Entities.Posts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.Handler
{
    public class EditNewCarPostHandler : IRequestHandler<EditNewCarPostCommand , Result<NewCarPost>>
    {
        private readonly IEditNewPostServices _carPostEditServices;
        private readonly IUserContextService _userContextService;
        private readonly INewCarPostServices _postService;
        private readonly IMapper _mapper;

        public EditNewCarPostHandler(
            IEditNewPostServices carPostEditServices,
            IUserContextService userContextService,
            INewCarPostServices postService,
            IMapper mapper)
        {
            _carPostEditServices = carPostEditServices;
            _userContextService = userContextService;
            _postService = postService;
            _mapper = mapper;
        }

        public async Task<Result<NewCarPost>> Handle(EditNewCarPostCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Result<NewCarPost>.Failure("User must be authenticated.", ErrorType.Unauthorized);

            // Step 3: Domain Validation
            var validation = await _carPostEditServices.ValidateEditAsync(request, userId, cancellationToken);
            if (!validation.IsSuccess)
                return validation;


            var carPost = _mapper.Map(request, validation.Value);
            if (carPost == null)
                return Result<NewCarPost>.Failure("Can't Map", ErrorType.BadRequest);

            carPost.SellerId = userId;

            await _postService.UpdateAsync(carPost);

            return Result<NewCarPost>.Success(carPost, "Updated Success");
        }
    }
}
