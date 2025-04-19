using MediatR;
using SharedKernel.Common.Results;

namespace SharedKernel.Application.Common.Interfaces;
interface ICommand<out TResponse> : IRequest<TResponse>
where TResponse : IResult
{
}
