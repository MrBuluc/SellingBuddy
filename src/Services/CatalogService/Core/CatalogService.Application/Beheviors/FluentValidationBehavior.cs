using FluentValidation;
using MediatR;

namespace CatalogService.Application.Beheviors
{
    public class FluentValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators = validators;

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            List<FluentValidation.Results.ValidationFailure> validationFailures = validators.Select(v => v.Validate(new ValidationContext<TRequest>(request))).SelectMany(result => result.Errors).GroupBy(validationFailure => validationFailure.ErrorMessage).Select(validationFailure => validationFailure.First()).Where(validationFailure => validationFailure is not null).ToList();

            if (validationFailures.Count != 0)
            {
                throw new ValidationException(validationFailures);
            }

            return next();
        }
    }
}
