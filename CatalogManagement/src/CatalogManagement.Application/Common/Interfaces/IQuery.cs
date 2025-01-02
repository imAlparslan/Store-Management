using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Common.Interfaces;
public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : IResult
{
}
