using MarketAPI.Application.Interfaces.INotification;

namespace MarketAPI.Application.Services.Notifications;

public class SmsNotificationService: INotificationService
{
    public async Task SendAsync(string message)
    {
        Console.WriteLine($"Dogrulama Sms'i: {message}");
        await Task.CompletedTask;
    }
}