using AppMonitor.Application.Repositories;
using AppMonitor.Application.Services;
using AppMonitor.Domain.Entities;
using AppMonitor.Infrastructure.Context;
using AppMonitor.Infrastructure.Services;

using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TargetAppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<TargetAppDbContext>();
builder.Services.AddScoped<ITargetAppService, TargetAppService>();

var emailSettings = builder.Configuration
                  .GetSection("EmailNotificationSettings")
                  .Get<EmailNotificationSettings>();

builder.Services.AddScoped<INotificationService>(sp =>
    new EmailNotificationService(
        emailSettings.Sender,
        emailSettings.Password,
        emailSettings.Host,
        emailSettings.Port));

builder.Services.AddScoped<ITargetAppRepository, TargetAppRepository>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
});


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
