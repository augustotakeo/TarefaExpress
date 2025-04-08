using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTOs;

public record TaskDTO(int Id, string Title, string? Description, DateTime CreatedAt, DateTime? CompletedAt, EStatus status)
{
    public static TaskDTO FromTask(TaskItem task)
    {
        return new TaskDTO(task.Id, task.Title, task.Description, task.CreatedAt, task.CompletedAt, task.Status);
    }
}