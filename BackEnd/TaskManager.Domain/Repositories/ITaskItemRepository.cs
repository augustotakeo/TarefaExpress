using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Repositories;

public interface ITaskItemRepository
{
    Task<TaskItem?> GetTaskItem(int id);

    Task<List<TaskItem>> GetTasksItens();

    Task<List<TaskItem>> GetTasksItens(EStatus status);

    Task CreateTaskItem(TaskItem task);

    Task UpdateTaskItem(TaskItem task);

    Task DeleteTaskItem(int id);
}