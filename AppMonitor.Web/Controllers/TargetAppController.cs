using AppMonitor.Application.Dtos;
using AppMonitor.Application.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace AppMonitor.Web.Controllers
{
    [Authorize]
    public class TargetAppController : Controller
    {
        private readonly ITargetAppService _appService;
        public TargetAppController(ITargetAppService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var apps = await _appService.GetAppsByUserAsync(userId);
            return View(apps);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppDto createAppDto)
        {

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                createAppDto.UserId = userId;
                await _appService.CreateAppAsync(createAppDto);


                return RedirectToAction("Index");
            }

            return View(createAppDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var app = await _appService.GetAppByIdAsync(id);
            if (app == null)
            {
                return NotFound();
            }

            var updateAppDto = new UpdateAppDto
            {
                Id = app.Id,
                Name = app.Name,
                Url = app.Url,
                Interval = app.Interval
            };

            return View(updateAppDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAppDto updateAppDto)
        {

            if (ModelState.IsValid)
            {
                await _appService.UpdateAppAsync(updateAppDto);
                return RedirectToAction("Index");
            }

            return View(updateAppDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _appService.DeleteAppAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Check(int id)
        {
            await _appService.CheckAppAsync(id);
            return RedirectToAction("Index");
        }
    }
}