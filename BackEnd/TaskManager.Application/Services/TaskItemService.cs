using TaskManager.Application.DTOs;
using TaskManager.Application.Extensions;
using TaskManager.Application.Requests;
using TaskManager.Application.Results;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Services;

public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _taskRepository;

    public TaskItemService(ITaskItemRepository taskRespository)
    {
        _taskRepository = taskRespository;
    }

    public async Task<Result> GetTaskItem(int id)
    {
        var taskItem = await _taskRepository.GetTaskItem(id);
        if (taskItem is null)
            return Result.Fail("Task not found");
        return Result.Ok(taskItem);
    }

    public async Task<Result> GetTasksItens()
    {
        var taskItens = await _taskRepository.GetTasksItens();

        return Result.Ok(TaskDTO.FromTasks(taskItens));
    }

    public async Task<Result> GetTaskItensByStatus(EStatus status)
    {
        var tasksItens = await _taskRepository.GetTasksItens(status);
        return Result.Ok(TaskDTO.FromTasks(tasksItens));
    }

    public async Task<Result> CreateTaskItem(CreateTaskRequest request)
    {
        var status = request.Status.GetEnumFromString<EStatus>(0);
        Console.WriteLine(request.Status);

        var taskItem = new TaskItem(request.Title, request.Description, status);

        if (taskItem.IsInvalid)
            return Result.Fail(taskItem.NotificationMessages, "Invalid Task");

        await _taskRepository.CreateTaskItem(taskItem);

        return Result.Ok(TaskDTO.FromTask(taskItem));
    }

    public async Task<Result> UpdateTaskItem(UpdateTaskRequest request)
    {
        var taskItem = await _taskRepository.GetTaskItem(request.Id);

        if (taskItem is null)
            return Result.Fail("Task not found");

        var status = request.Status.GetEnumFromString<EStatus>(0);

        taskItem.Update(request.Title, request.Description, status);

        if (taskItem.IsInvalid)
            return Result.Fail(taskItem.NotificationMessages, "Invalid Task");

        await _taskRepository.UpdateTaskItem(taskItem);

        return Result.Ok(TaskDTO.FromTask(taskItem));
    }

    public async Task<Result> DeleteTaskItem(int id)
    {
        await _taskRepository.DeleteTaskItem(id);
        return Result.Ok();
    }
}