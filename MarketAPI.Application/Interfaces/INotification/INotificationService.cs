namespace MarketAPI.Application.Interfaces.INotification;

public interface INotificationService
{
    Task SendAsync(string message);
}