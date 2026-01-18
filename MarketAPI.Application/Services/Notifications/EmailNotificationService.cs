using MarketAPI.Application.Interfaces.INotification;

namespace MarketAPI.Application.Services.Notifications;

public class EmailNotificationService: INotificationService
{
    public async Task SendAsync(string message)
    {
        Console.WriteLine($"Email dogrulamasi:{message}");
        await Task.CompletedTask;
    }
}