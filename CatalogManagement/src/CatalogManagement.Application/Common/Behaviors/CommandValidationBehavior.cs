using CatalogManagement.SharedKernel;
using FluentValidation;
using MediatR;

namespace CatalogManagement.Application.Common.Behaviors;
public class CommandValidationBehavior<TCommand, TResponse>
    : IPipelineBehavior<TCommand, TResponse>
    where TCommand : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;

    public CommandValidationBehavior(IEnumerable<IValidator<TCommand>> validator)
    {
        _validators = validator;
    }

    public async Task<TResponse> Handle(TCommand request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        var context = new ValidationContext<TCommand>(request);

        var validationResult = await Task.WhenAll(
            _validators.Select(async x => await x.ValidateAsync(context)));

        var errors = validationResult.Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(x => CreateValidationError(x.PropertyName, x.ErrorMessage))
            .ToList();

        if (errors.Count != 0)
        {
            return (dynamic)errors;
        }
        return await next();
    }

    private Error CreateValidationError(string propertyName, string errorMessage)
    {
        return new Error(propertyName, ErrorType.Validation, errorMessage);
    }
}
