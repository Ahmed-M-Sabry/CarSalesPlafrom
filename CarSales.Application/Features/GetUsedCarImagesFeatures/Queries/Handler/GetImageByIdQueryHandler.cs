using CarSales.Application.Common;
using CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Models;
using CarSales.Application.IServices;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Handler
{
    public class GetImageByIdQueryHandler : IRequestHandler<GetImageByIdQurey, Result<UsedCarImage>>
{
    private readonly IUsedCarImageServices _usedCarImageServices;
    private readonly IOldCarPostService _postService;
    private readonly IUserContextService _userContextService;

    public GetImageByIdQueryHandler(
        IUsedCarImageServices usedCarImageServices,
        IOldCarPostService postService,
        IUserContextService userContextService)
    {
        _usedCarImageServices = usedCarImageServices;
        _postService = postService;
        _userContextService = userContextService;
    }

    public async Task<Result<UsedCarImage>> Handle(GetImageByIdQurey request, CancellationToken cancellationToken)
    {
        var userId = _userContextService.GetUserId();
        if (string.IsNullOrEmpty(userId))
            return Result<UsedCarImage>.Failure("User must be authenticated to get an image.", ErrorType.Unauthorized);

        // Get the image
        var image = await _usedCarImageServices.GetByIdAsync(request.ImageId);
        if (image == null)
            return Result<UsedCarImage>.Failure("Image not found.", ErrorType.NotFound);

        // Check if the user owns the post associated with the image
        var postResult = await _postService.GetByIdAsync(userId, image.OldCarPostId, cancellationToken);
        if (!postResult.IsSuccess)
            return Result<UsedCarImage>.Failure("You are not authorized to access this image.", ErrorType.Unauthorized);

        return Result<UsedCarImage>.Success(image);
    }
}
}
