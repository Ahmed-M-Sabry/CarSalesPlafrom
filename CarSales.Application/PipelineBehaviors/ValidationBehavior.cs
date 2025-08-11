using FluentValidation;
using MediatR;
using CarSales.Application.Common;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.PipelineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request,RequestHandlerDelegate<TResponse> next,CancellationToken cancellationToken)
        {
            if (_validator != null)
            {
                var result = await _validator.ValidateAsync(request, cancellationToken);

                if (!result.IsValid)
                {
                    var errors = string.Join(" | ", result.Errors.Select(e => e.ErrorMessage));
                    return Result<TResponse>.Failure(errors, ErrorType.BadRequest) as TResponse;
                }
            }

            return await next();
        }
    }
}
