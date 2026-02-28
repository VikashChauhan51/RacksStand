namespace RacksStands.Framework.Cqrs.Abstractions;

public interface ICommandHandler<in TCommand, TResponse> : IHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>;

public interface ICommandHandler<in TCommand> : IHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>;
