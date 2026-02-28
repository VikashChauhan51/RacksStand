using System.Threading;
using System.Threading.Tasks;

namespace RacksStands.Framework.Cqrs.Abstractions;

public interface IHandler<in TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken ct = default);

}
