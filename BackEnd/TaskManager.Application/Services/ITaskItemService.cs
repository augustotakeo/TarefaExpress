using TaskManager.Application.Requests;
using TaskManager.Application.Results;

namespace TaskManager.Application.Services;

public interface ITaskItemService
{
    Task<Result> GetTaskItem(int id);
    Task<Result> GetTasksItens();
    Task<Result> CreateTaskItem(CreateTaskRequest request);
    Task<Result> UpdateTaskItem(UpdateTaskRequest request);
    Task<Result> DeleteTaskItem(int id);
}