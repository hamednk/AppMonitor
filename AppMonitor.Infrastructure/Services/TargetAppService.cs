using AppMonitor.Application.Dtos;
using AppMonitor.Application.Repositories;
using AppMonitor.Application.Services;
using AppMonitor.Domain.Entities;

using AutoMapper;

namespace AppMonitor.Infrastructure.Services
{
    public class TargetAppService : ITargetAppService
    {
        private readonly ITargetAppRepository _appRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public TargetAppService(ITargetAppRepository appRepository, INotificationService notificationService, IMapper mapper)
        {
            _appRepository = appRepository;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TargetAppDto>> GetAppsByUserAsync(string userId)
        {

            var apps = await _appRepository.GetAppsByUserAsync(userId);
            return _mapper.Map<IEnumerable<TargetAppDto>>(apps);
        }

        public async Task<TargetAppDto> GetAppByIdAsync(int id)
        {

            var app = await _appRepository.GetAppByIdAsync(id);
            return _mapper.Map<TargetAppDto>(app);
        }

        public async Task CreateAppAsync(CreateAppDto createAppDto)
        {

            var app = _mapper.Map<TargetApp>(createAppDto);
            await _appRepository.CreateAppAsync(app);
        }

        public async Task UpdateAppAsync(UpdateAppDto updateAppDto)
        {

            var app = _mapper.Map<TargetApp>(updateAppDto);
            await _appRepository.UpdateAppAsync(app);
        }

        public async Task DeleteAppAsync(int id)
        {

            await _appRepository.DeleteAppAsync(id);
        }

        public async Task CheckAppAsync(int id)
        {

            var app = await _appRepository.GetAppByIdAsync(id);
            if (app != null)
            {

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(app.Url);
                    var statusCode = (int)response.StatusCode;


                    if (statusCode >= 200 && statusCode < 300)
                    {
                        app.IsUp = true;
                    }
                    else
                    {
                        app.IsUp = false;


                        await _notificationService.SendNotificationAsync(app);
                    }
                    app.LastChecked = DateTime.Now;


                    await _appRepository.UpdateAppAsync(app);
                }
            }
        }
    }
}