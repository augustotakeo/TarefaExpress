using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories;

public interface ITaskItemRepository
{
    Task<TaskItem?> GetTaskItem(int id);

    Task<List<TaskItem>> GetTasksItens();

    Task CreateTaskItem(TaskItem task);

    Task UpdateTaskItem(TaskItem task);

    Task DeleteTaskItem(int id);
}