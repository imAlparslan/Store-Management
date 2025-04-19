using MediatR;
using SharedKernel.Common.Results;

namespace SharedKernel.Application.Common.Interfaces;
interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : IResult
{
}
