using System.Threading;
using System.Threading.Tasks;

namespace RacksStands.Framework.Cqrs.Abstractions;

public delegate Task<TResponse> RequestHandlerDelegate<TResponse>(
    CancellationToken ct = default);
