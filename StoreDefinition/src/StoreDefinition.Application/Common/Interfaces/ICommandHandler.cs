using MediatR;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Common.Interfaces;
public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : IResult
{
}
