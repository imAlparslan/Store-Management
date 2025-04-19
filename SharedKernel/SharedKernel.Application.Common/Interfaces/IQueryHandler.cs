using MediatR;
using SharedKernel.Common.Results;

namespace SharedKernel.Application.Common.Interfaces;
public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQuery<TResponse>
    where TResponse : IResult
{
}
