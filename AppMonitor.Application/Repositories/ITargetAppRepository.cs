using AppMonitor.Domain.Entities;

namespace AppMonitor.Application.Repositories
{
    public interface ITargetAppRepository
    {
        Task<IEnumerable<TargetApp>> GetAppsByUserAsync(string userId);
        Task<TargetApp> GetAppByIdAsync(int id);
        Task CreateAppAsync(TargetApp app);
        Task UpdateAppAsync(TargetApp app);
        Task DeleteAppAsync(int id);
    }
}
