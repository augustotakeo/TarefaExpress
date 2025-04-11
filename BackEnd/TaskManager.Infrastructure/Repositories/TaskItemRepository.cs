using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Infrastructure.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly TaskContext _taskContext;

    public TaskItemRepository(TaskContext taskContext)
    {
        _taskContext = taskContext;
    }

    public async Task<TaskItem?> GetTaskItem(int id)
    {
        var taskItem = await _taskContext.TaskItens
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return taskItem;
    }

    public async Task<List<TaskItem>> GetTasksItens()
    {
        var taskItens = await _taskContext.TaskItens.ToListAsync();
        return taskItens;
    }

    public async Task<List<TaskItem>> GetTasksItens(Expression<Func<TaskItem, bool>> filter)
    {
        var taskItens = await _taskContext.TaskItens
            .Where(filter)
            .ToListAsync();
            
        return taskItens;
    }

    public async Task CreateTaskItem(TaskItem task)
    {
        _taskContext.Add(task);
        await _taskContext.SaveChangesAsync();
    }

    public async Task UpdateTaskItem(TaskItem taskItem)
    {
        _taskContext.Update(taskItem);
        await _taskContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteTaskItem(int id)
    {
        var n = await _taskContext.TaskItens
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
        return n > 0;
    }
}