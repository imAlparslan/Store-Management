using MediatR;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Common.Interfaces;
public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQuery<TResponse>
    where TResponse : IResult
{
}
