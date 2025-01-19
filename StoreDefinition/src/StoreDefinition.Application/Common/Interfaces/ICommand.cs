using MediatR;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Common.Interfaces;
public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse : IResult
{
}