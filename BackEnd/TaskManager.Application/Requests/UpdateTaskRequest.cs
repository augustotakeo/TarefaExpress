using TaskManager.Domain.Enums;

namespace TaskManager.Application.Requests;

public class UpdateTaskRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
}