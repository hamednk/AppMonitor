using AppMonitor.Application.Dtos;
using AppMonitor.Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppMonitor.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager; 
        private readonly SignInManager<User> _signInManager; 

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            
            if (ModelState.IsValid)
            {
                
                var user = new User
                {
                    UserName = userDto.UserName,
                    Email = userDto.Email
                };

                
                var result = await _userManager.CreateAsync(user, "Password123!");

                if (result.Succeeded)
                {
                    
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "TargetApp");
                }

                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            
            return View(userDto);
        }

        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userDto.UserName, "Password123!", false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "TargetApp");
                }
                
                ModelState.AddModelError("", "Invalid login attempt");
            }

            
            return View(userDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            
            await _signInManager.SignOutAsync();

            
            return RedirectToAction("Login", "Account");
        }
    }
}