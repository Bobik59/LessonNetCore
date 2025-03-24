using Microsoft.EntityFrameworkCore;
using DataAccess.Repository.EntityFramework;
using DataAccess.Repository.Abstract;
using System;
using DataAccess;

var builder = WebApplication.CreateBuilder(args);

string connectionString = @"Server=(localdb)\MSSQLLocalDB; Database=Cinema; Trusted_Connection=True;";
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapGet("/", () => "Hello World!");

app.Run();
