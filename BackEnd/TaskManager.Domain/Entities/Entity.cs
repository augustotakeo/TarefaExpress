using Flunt.Notifications;

namespace TaskManager.Domain.Entities;

public abstract class Entity : Notifiable<Notification>
{
    public bool IsInvalid => !IsValid;

    public IReadOnlyList<string> NotificationMessages => [.. Notifications.Select(x => x.Message)];

    public int Id { get; private set; }

    protected void AddNotificationIfInvalidEnum(Enum @enum, string key, string message)
    {
        foreach (var value in @enum.GetType().GetEnumValues())
        {
            if (@enum.Equals((Enum)value))
                return;
        }

        AddNotification(key, message);
    }

}