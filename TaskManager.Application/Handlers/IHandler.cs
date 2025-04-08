using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers;

public interface IHandler<T> where T : ICommand
{
    Task<ICommandResult> Handle(T command);
}