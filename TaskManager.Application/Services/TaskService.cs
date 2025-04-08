using TaskManager.Application.DTOs;
using TaskManager.Application.Requests;
using TaskManager.Application.Results;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRespository _taskRepository;

    public TaskService(ITaskRespository taskRespository)
    {
        _taskRepository = taskRespository;
    }

    public async Task<Result> GetTaskItem(int id)
    {
        var task = await _taskRepository.GetTaskItem(id);
        if(task is null)
            return Result.Fail("Task not found");
        return Result.Ok(task);
    }

    public async Task<Result> GetTasksItens()
    {
        var tasks = await _taskRepository.GetTasksItens();
        return Result.Ok(tasks);
    }

    public async Task<Result> CreateTaskItem(CreateTaskRequest request)
    {
        var task = new TaskItem(request.Title, request.Description, request.Status, request.CompletedAt);

        if (task.IsInvalid)
            return Result.Fail(task.NotificationMessages, "Invalid Task");

        await _taskRepository.CreateTaskItem(task);

        return Result.Ok(TaskDTO.FromTask(task));
    }

    public async Task<Result> UpdateTaskItem(UpdateTaskRequest request)
    {
        var task = await _taskRepository.GetTaskItem(request.Id);

        if (task is null)
            return Result.Fail("Task not found");

        if (task.IsInvalid)
            return Result.Fail(task.NotificationMessages, "Invalid Task");

        task.Update(request.Title, request.Description, request.Status, request.CompletedAt);

        await _taskRepository.UpdateTaskItem(task);

        return Result.Ok(TaskDTO.FromTask(task));
    }

    public async Task<Result> DeleteTaskItem(int id)
    {
        await _taskRepository.DeleteTaskItem(id);
        return Result.Ok();
    }
}