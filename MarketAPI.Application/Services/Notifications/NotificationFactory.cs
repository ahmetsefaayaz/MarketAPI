using MarketAPI.Application.Interfaces.INotification;

namespace MarketAPI.Application.Services.Notifications;

public static class NotificationFactory
{
    public static INotificationService Create(string type)
    {
        return type?.ToLower() switch
        {
            "sms" => new SmsNotificationService(),
            "email" => new EmailNotificationService(),
            _ => throw new ArgumentException("Invalid notification type", nameof(type))
        };
    }
}