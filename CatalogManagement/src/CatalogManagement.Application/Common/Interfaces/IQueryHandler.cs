using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Common.Interfaces;
public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQuery<TResponse>
    where TResponse : IResult
{
}
