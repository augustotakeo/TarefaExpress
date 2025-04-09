using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Requests;
using TaskManager.Application.Services;

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

    [HttpGet]
    public async Task<IActionResult> GetTaskItens()
    {
        var result = await _taskItemService.GetTasksItens();
        if (result.Success)
            return Ok(result.Content);
        return BadRequest(result.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskItem([FromRoute] int id)
    {
        var result = await _taskItemService.GetTaskItem(id);
        if (result.Success)
            return Ok(result.Content);
        return NotFound(result.Message);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskItem([FromBody] CreateTaskRequest request)
    {
        var result = await _taskItemService.CreateTaskItem(request);
        if (result.Success)
            return Ok(result.Content);
        return BadRequest(result.Message);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTaskItem([FromBody] UpdateTaskRequest request)
    {
        var result = await _taskItemService.UpdateTaskItem(request);
        if (result.Success)
            return Ok(result.Content);
        return BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskItem([FromRoute] int id)
    {
        var result = await _taskItemService.DeleteTaskItem(id);
        if (result.Success)
            return NoContent();
        return BadRequest();
    }

}