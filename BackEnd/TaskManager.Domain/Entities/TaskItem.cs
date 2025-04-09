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

    public TaskItem(string title, string? description, EStatus status, DateTime? completedAt = null)
    {
        Title = title;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        CompletedAt = completedAt;
        Status = status;
        CheckProperties();        
    }

    public void Update(string title, string? description, EStatus status, DateTime? completedAt = null)
    {
        Title = title;
        Description = description;
        CompletedAt = completedAt;
        Status = status;
        CheckProperties();        
    }

    private void CheckProperties()
    {
        AddNotifications(new Contract<Notification>()
            .IsNotNullOrWhiteSpace(Title, "TaskItem.Title")
            .IsLowerOrEqualsThan(Title, 100, "TaskItem.Title")
            .IsGreaterOrEqualsThan(CompletedAt ?? CreatedAt, CreatedAt, "TaskItem.CompletedAt"));
    }
}