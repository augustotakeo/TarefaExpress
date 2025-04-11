using TaskManager.Application.Extensions;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.DTOs;

public record TaskDTO(
    int Id, 
    string Title, 
    string? Description, 
    DateTime CreatedAt, 
    DateTime? CompletedAt, 
    string? Status)
{
    public static TaskDTO FromTask(TaskItem task)
    {
        return new TaskDTO(
            task.Id, 
            task.Title, 
            task.Description, 
            task.CreatedAt, 
            task.CompletedAt, 
            task.Status.ToString());
    }

    public static List<TaskDTO> FromTasks(IEnumerable<TaskItem> tasks)
    {
        return tasks.Select(FromTask).ToList();
    }
}