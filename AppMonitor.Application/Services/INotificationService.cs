using AppMonitor.Domain.Entities;

namespace AppMonitor.Application.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(TargetApp app);
    }
}