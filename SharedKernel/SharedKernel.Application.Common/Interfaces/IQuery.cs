using MediatR;
using SharedKernel.Common.Results;

namespace SharedKernel.Application.Common.Interfaces;
public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : IResult
{
}
