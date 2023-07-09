using AppMonitor.Application.Dtos;

namespace AppMonitor.Application.Services
{
    public interface ITargetAppService
    {
        Task<IEnumerable<TargetAppDto>> GetAppsByUserAsync(string userId);
        Task<TargetAppDto> GetAppByIdAsync(int id);
        Task CreateAppAsync(CreateAppDto createAppDto);
        Task UpdateAppAsync(UpdateAppDto updateAppDto);
        Task DeleteAppAsync(int id);
        Task CheckAppAsync(int id);
    }
}
