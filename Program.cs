using LessonNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IPasswordVerifier, PasswordVerifier>();

var app = builder.Build();

app.UseMiddleware<AuthMiddleware>();
app.Run();