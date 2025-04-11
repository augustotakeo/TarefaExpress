using System.Linq.Expressions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;

namespace TaskManager.Tests.Fakes;

public class FakeTaskItemRepository : ITaskItemRepository
{
    private readonly object? _returnValue;

    public FakeTaskItemRepository() { }

    public FakeTaskItemRepository(object? returnValue)
    {
        _returnValue = returnValue;
    }

    public async Task CreateTaskItem(TaskItem task)
    {
    }

    public async Task<bool> DeleteTaskItem(int id)
    {
        return (bool)_returnValue;
    }

    public async Task<TaskItem?> GetTaskItem(int id)
    {
        return (TaskItem)_returnValue;
    }

    public async Task<List<TaskItem>> GetTasksItens()
    {
        return (List<TaskItem>)_returnValue;
    }

    public async Task<List<TaskItem>> GetTasksItens(Expression<Func<TaskItem, bool>> filter)
    {
        if (_returnValue is null)
            return null;
        var taskItens = (IQueryable<TaskItem>)_returnValue;
        return taskItens.Where(filter).ToList();
    }

    public async Task UpdateTaskItem(TaskItem task)
    {
    }
}