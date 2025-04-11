using System.Threading.Tasks;
using TaskManager.Application.DTOs;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Requests;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;
using TaskManager.Tests.Fakes;

namespace TaskManager.Tests.Application.Services;

public class TaskItemServiceTests
{

    [Fact]
    public async Task GetTaskItem_ExistingTaskItem_ReturnOkResult()
    {
        var taskItem = new TaskItem("Titulo", "Descrição", EStatus.Pendente);
        var repository = new FakeTaskItemRepository(taskItem);
        var service = new TaskItemService(repository);
        var result = await service.GetTaskItem(1);
        Assert.True(result.Success);
        Assert.NotNull(taskItem);
    }

    [Fact]
    public async Task GetTaskItem_NotFoundTaskItem_ReturnFailResult()
    {
        var repository = new FakeTaskItemRepository(null);
        var service = new TaskItemService(repository);
        var result = await service.GetTaskItem(10);
        Assert.False(result.Success);
        Assert.Equal("Tarefa não encontrada", result.Value);
    }

    [Fact]
    public async Task GetTasksItens_NullResult_ThrowDataAccessException()
    {
        var repository = new FakeTaskItemRepository(null);
        var service = new TaskItemService(repository);
        await Assert.ThrowsAsync<DataAccessException>(service.GetTasksItens);
    }

    [Fact]
    public async Task GetTaskItens_NotNullResult_ReturnOkResult()
    {
        List<TaskItem> tasksItens = [new TaskItem("Titulo", "Descrição", EStatus.Pendente)];
        var repository = new FakeTaskItemRepository(tasksItens);
        var service = new TaskItemService(repository);
        var result = await service.GetTasksItens();
        Assert.True(result.Success);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public async Task GetTaskItensByStatus_PassStatusPendente_Return3TasksItens()
    {
        IQueryable<TaskItem> taskItens = new List<TaskItem>
        {
            new("Task 1", "Description for Task 1", EStatus.Pendente),
            new("Task 2", "Description for Task 2", EStatus.EmProgresso),
            new("Task 3", "Description for Task 3", EStatus.Concluido),
            new("Task 4", "Description for Task 4", EStatus.EmProgresso),
            new("Task 5", "Description for Task 5", EStatus.Pendente),
            new("Task 6", "Description for Task 6", EStatus.Concluido),
            new("Task 7", "Description for Task 7", EStatus.Pendente),
            new("Task 8", "Description for Task 8", EStatus.Concluido),
            new("Task 9", "Description for Task 9", EStatus.EmProgresso),
            new("Task 10", "Description for Task 10", EStatus.EmProgresso)
        }.AsQueryable();

        var repository = new FakeTaskItemRepository(taskItens);
        var service = new TaskItemService(repository);
        var result = await service.GetTaskItensByStatus(EStatus.Pendente);
        var resultTaskItens = result.Value as List<TaskDTO>;
        Assert.Equal(3, resultTaskItens?.Count);
    }

    [Fact]
    public async Task GetTaskItensByStatus_NullResult_ThrowDataAccessException()
    {
        var repository = new FakeTaskItemRepository(null);
        var service = new TaskItemService(repository);
        await Assert.ThrowsAsync<DataAccessException>(() => service.GetTaskItensByStatus(EStatus.EmProgresso));
    }

    [Fact]
    public async Task CreateTaskItem_ValidRequest_ReturnOkResult()
    {
        var repository = new FakeTaskItemRepository();
        var service = new TaskItemService(repository);
        var request = new CreateTaskRequest
        {
            Title = "Titulo Valido",
            Description = "Descrição Válida",
            Status = EStatus.Pendente.ToString()
        };
        var result = await service.CreateTaskItem(request);
        Assert.True(result.Success);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public async Task CreateTaskItem_InvalidStatus_ReturnFailResult()
    {
        var repository = new FakeTaskItemRepository();
        var service = new TaskItemService(repository);
        var request = new CreateTaskRequest
        {
            Title = "Titulo Valido",
            Description = "Descrição Válida",
            Status = "0"
        };
        var result = await service.CreateTaskItem(request);
        Assert.False(result.Success);
        var resulValue = result.Value as IReadOnlyList<string>;
        Assert.Equal("Status inválido", resulValue?.First());
    }

    [Fact]
    public async Task UpdateTaskItem_ValidRequest_ReturnOkResult()
    {
        var taskItem = new TaskItem("Primeiro Titulo", "Primeira Descrição", EStatus.Pendente);
        var repository = new FakeTaskItemRepository(taskItem);
        var service = new TaskItemService(repository);
        var request = new UpdateTaskRequest
        {
            Id = 100,
            Title = "Titulo Valido",
            Description = "Descrição Válida",
            Status = EStatus.Pendente.ToString()
        };
        var result = await service.UpdateTaskItem(request);
        Assert.True(result.Success);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public async Task UpdateTaskItem_NotFoundTaskItem_ReturnFailResult()
    {
        var repository = new FakeTaskItemRepository(null);
        var service = new TaskItemService(repository);
        var request = new UpdateTaskRequest
        {
            Id = 100,
            Title = "Titulo Valido",
            Description = "Descrição Válida",
            Status = EStatus.Pendente.ToString()
        };
        var result = await service.UpdateTaskItem(request);
        Assert.False(result.Success);
        Assert.Equal("Tarefa não econtrada", result.Value);
    }

    [Fact]
    public async Task UpdateTaskItem_InvalidTitle_ReturnFailResult()
    {
        var taskItem = new TaskItem("Primeiro Titulo", "Primeira Descrição", EStatus.Pendente);
        var repository = new FakeTaskItemRepository(taskItem);
        var service = new TaskItemService(repository);
        var request = new UpdateTaskRequest
        {
            Id = 100,
            Title = "",
            Description = "Descrição Válida",
            Status = EStatus.Pendente.ToString()
        };
        var result = await service.UpdateTaskItem(request);
        Assert.False(result.Success);
        var resulValue = result.Value as IReadOnlyList<string>;
        Assert.Equal("Título dever entre 1 e 100 caracteres", resulValue?.First());
    }

    [Fact]
    public async Task DeleteTaskItem_ExistingTaskItem_ReturnOkResult()
    {
        var repository = new FakeTaskItemRepository(true);
        var service = new TaskItemService(repository);
        var result = await service.DeleteTaskItem(1);
        Assert.True(result.Success);
        Assert.Equal("Excluído com sucesso", result.Value);
    }

    [Fact]
    public async Task DeleteTaskItem_NotFoundTaskItem_ReturnFailResult()
    {
        var repository = new FakeTaskItemRepository(false);
        var service = new TaskItemService(repository);
        var result = await service.DeleteTaskItem(10);
        Assert.False(result.Success);
        Assert.Equal("Tarefa não encontrada", result.Value);
    }


    // CreateTaskItem
    // UpdateTaskItem
    // DeleteTaskItem
}