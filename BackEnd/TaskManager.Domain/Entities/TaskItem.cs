using Flunt.Notifications;
using Flunt.Validations;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public class TaskItem : Entity
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public EStatus Status { get; private set; }

    public TaskItem(string title, string? description, EStatus status)
    {
        Title = title;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdateStatus(status);
        CheckProperties();
    }

    public void Update(string title, string? description, EStatus status)
    {
        Title = title;
        Description = description;
        UpdateStatus(status);
        CheckProperties();
    }

    public void UpdateStatus(EStatus status)
    {
        if (Status == status)
            return;

        Status = status;
        if (Status == EStatus.Concluido)
            CompletedAt = DateTime.UtcNow;
        else
            CompletedAt = null;
    }

    private void CheckProperties()
    {
        AddNotifications(new Contract<Notification>()
            .IsNotNullOrWhiteSpace(Title, "TaskItem.Title", "Preencher um título")
            .IsBetween(Title.Length, 1, 100, "TaskItem.Title", "Título dever entre 1 e 100 caracteres")
            .IsGreaterOrEqualsThan(CompletedAt ?? CreatedAt, CreatedAt, "TaskItem.CompletedAt", "Data de conclusão inválida"));

        AddNotificationIfInvalidEnum(Status, "TaskItem.Status", "Status inválido");
    }
}