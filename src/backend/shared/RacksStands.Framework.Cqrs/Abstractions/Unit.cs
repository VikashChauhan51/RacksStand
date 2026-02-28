using System.Threading.Tasks;

namespace RacksStands.Framework.Cqrs.Abstractions;

public readonly record struct Unit
{
    public static readonly Unit Value = new();

    public static Task<Unit> Task => System.Threading.Tasks.Task.FromResult(Value);
}
