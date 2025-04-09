using Flunt.Notifications;

namespace TaskManager.Domain.Entities;

public abstract class Entity : Notifiable<Notification>
{
    public bool IsInvalid => !IsValid;

    public IReadOnlyList<string> NotificationMessages => [.. Notifications.Select(x => x.Message)];

    public int Id { get; private set; }


}