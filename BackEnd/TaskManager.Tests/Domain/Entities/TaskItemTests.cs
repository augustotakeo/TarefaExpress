using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Tests.Domain.Entities;

public class TaskItemTests
{
    [Theory]
    [InlineData("Tarefa de Matemática", "Resolver lista de questões", EStatus.Pendente)]
    [InlineData("Tarefa de Inglês", null, EStatus.EmProgresso)]
    public void Constructor_ValidInput_ReturnValidEntity(string title, string? description, EStatus status)
    {
        var taskItem = new TaskItem(title, description, status);
        Assert.True(taskItem.IsValid);
    }

    [Theory]
    [InlineData("", "Descrição 01", EStatus.Pendente)]
    [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse in luctus arcu, id tincidunt purus.", null, EStatus.Pendente)]
    public void Constructor_InvalidInput_ReturnInvalidEntity(string title, string? description, EStatus status)
    {
        var taskItem = new TaskItem(title, description, status);
        Assert.True(taskItem.IsInvalid);
    }

    [Theory]
    [InlineData("Tarefa de Matemática", "Resolver lista de questões", EStatus.Pendente)]
    [InlineData("Tarefa de Inglês", null, EStatus.EmProgresso)]
    public void Update_ValidInput_ReturnValidEntity(string title, string? description, EStatus status)
    {
        var taskItem = CreateValidTaskItem();
        taskItem.Update(title, description, status);
        Assert.True(taskItem.IsValid);
    }

    [Theory]
    [InlineData("", "Descrição 01", EStatus.Pendente)]
    [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse in luctus arcu, id tincidunt purus.", null, EStatus.Pendente)]
    public void Update_InvalidInput_ReturnInvalidEntity(string title, string? description, EStatus status)
    {
        var taskItem = CreateValidTaskItem();
        taskItem.Update(title, description, status);
        Assert.True(taskItem.IsInvalid);
    }

    [Fact]
    public void UpdateStatus_ValidInput_ReturnValidEntity()
    {
        var taskItem = CreateValidTaskItem();
        taskItem.UpdateStatus(EStatus.Concluido);
        Assert.True(taskItem.IsValid);
    }

    [Fact]
    public void UpdateStatus_InvalidInput_ReturnInvalidEntity()
    {
        var taskItem = CreateValidTaskItem();
        taskItem.UpdateStatus(0);
        Assert.True(taskItem.IsInvalid);
    }

    private TaskItem CreateValidTaskItem()
    {
        return new("Tarefa de Estrutura de Dados", "Problema das moedas", EStatus.Pendente);
    }

}