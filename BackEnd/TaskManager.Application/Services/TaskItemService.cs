using System.Linq.Expressions;
using TaskManager.Application.DTOs;
using TaskManager.Application.Exceptions;
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
            return Result.Fail("Tarefa não encontrada");
        return Result.Ok(TaskDTO.FromTask(taskItem));
    }

    public async Task<Result> GetTasksItens()
    {
        var taskItens = await _taskRepository.GetTasksItens();

        DataAccessException.ThrowsIfNull(taskItens, "Não foi possível obter as tarefas");

        var orderedTaskItens = taskItens.OrderBy(x => x.CreatedAt);

        return Result.Ok(TaskDTO.FromTasks(orderedTaskItens));
    }

    public async Task<Result> GetTaskItensByStatus(EStatus status)
    {
        Expression<Func<TaskItem, bool>> filter = taskItem => taskItem.Status == status;

        var tasksItens = await _taskRepository.GetTasksItens(filter);

        DataAccessException.ThrowsIfNull(tasksItens, "Não foi possível obter as tarefas");

        return Result.Ok(TaskDTO.FromTasks(tasksItens));
    }

    public async Task<Result> CreateTaskItem(CreateTaskRequest request)
    {
        var status = request.Status.GetEnumFromString<EStatus>(0);

        var taskItem = new TaskItem(request.Title, request.Description, status);

        if (taskItem.IsInvalid)
            return Result.Fail(taskItem.NotificationMessages);

        await _taskRepository.CreateTaskItem(taskItem);

        return Result.Ok(TaskDTO.FromTask(taskItem));
    }

    public async Task<Result> UpdateTaskItem(UpdateTaskRequest request)
    {
        var taskItem = await _taskRepository.GetTaskItem(request.Id);

        if (taskItem is null)
            return Result.Fail("Tarefa não econtrada");

        var status = request.Status.GetEnumFromString<EStatus>(0);

        taskItem.Update(request.Title, request.Description, status);

        if (taskItem.IsInvalid)
            return Result.Fail(taskItem.NotificationMessages);

        await _taskRepository.UpdateTaskItem(taskItem);

        return Result.Ok(TaskDTO.FromTask(taskItem));
    }

    public async Task<Result> DeleteTaskItem(int id)
    {
        var taskItemDeleted = await _taskRepository.DeleteTaskItem(id);
        if(taskItemDeleted)
            return Result.Ok("Excluído com sucesso");
        return Result.Fail("Tarefa não encontrada");
    }
}