using MediatR;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Common.Interfaces;
public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : IResult
{
}
