using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers;

public class CreateTaskCommandHandler : IHandler<CreateTaskCommand>
{
    public async Task<ICommandResult> Handle(CreateTaskCommand command)
    {
        

        return null;
    }
}