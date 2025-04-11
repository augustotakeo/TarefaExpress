using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Requests;
using TaskManager.Application.Results;
using TaskManager.Application.Services;
using TaskManager.Domain.Enums;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("tasks")]
public class TaskItemController : ControllerBase
{
    private readonly ITaskItemService _taskItemService;

    public TaskItemController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }

    private IActionResult GetActionResult(Result result)
    {
        if(result.Success)
            return Ok(result.Value);
        return BadRequest(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetTaskItens()
    {
        var result = await _taskItemService.GetTasksItens();
        return GetActionResult(result);
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetTaskItensByStatus([FromRoute] EStatus status)
    {
        var result = await _taskItemService.GetTaskItensByStatus(status);
        return GetActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskItem([FromRoute] int id)
    {
        var result = await _taskItemService.GetTaskItem(id);
        return GetActionResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskItem([FromBody] CreateTaskRequest request)
    {
        var result = await _taskItemService.CreateTaskItem(request);
        return GetActionResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTaskItem([FromBody] UpdateTaskRequest request)
    {
        var result = await _taskItemService.UpdateTaskItem(request);
        return GetActionResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskItem([FromRoute] int id)
    {
        var result = await _taskItemService.DeleteTaskItem(id);
        return GetActionResult(result);
    }

}