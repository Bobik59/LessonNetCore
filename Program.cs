using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LessonNetCore.Services;
using LessonNetCore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Добавляем Razor Pages
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IEncryptionService, EncryptionService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

app.UseRouting();

// Настроить маршруты для Razor Pages
app.MapRazorPages();

app.Run();