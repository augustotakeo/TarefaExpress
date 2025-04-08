using TaskManager.Domain.Enums;

namespace TaskManager.Application.Commands;

public class CreateTaskCommand : ICommand
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? CompletedAt { get; set; }
    public EStatus Status { get; set; }
}