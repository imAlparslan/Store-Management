using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Common.Interfaces;
public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : IResult
{
}
