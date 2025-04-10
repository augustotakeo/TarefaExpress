using TaskManager.Application.Requests;
using TaskManager.Application.Results;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Services;

public interface ITaskItemService
{
    Task<Result> GetTaskItem(int id);
    Task<Result> GetTasksItens();
    Task<Result> GetTaskItensByStatus(EStatus status);
    Task<Result> CreateTaskItem(CreateTaskRequest request);
    Task<Result> UpdateTaskItem(UpdateTaskRequest request);
    Task<Result> DeleteTaskItem(int id);
}