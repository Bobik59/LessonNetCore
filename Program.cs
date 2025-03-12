using DataAccess;
using DataAccess.Models;
using DataAccess.Repository.Abstract;
using DataAccess.Repository.EntityFramework;
using LessonNetCore.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder();

// ��������� ���� ������
var connString =
    @"Server=(localdb)\MSSQLLocalDB; Database=StudentDb; Trusted_Connection=True;";
builder.Services.AddDbContext<DbContextStudent>(options
    => options.UseSqlServer(connString));
builder.Services.AddScoped<IStudentRepository, EntityFrameworkStudent>();

builder.Services.AddScoped<RequestHandlerService>();

var app = builder.Build();

app.MapGet("/students",
    async (RequestHandlerService requestHandler) =>
    await requestHandler.GetAllStudentsAsync());
app.MapGet("/student/{id:int}",
    async (int id, RequestHandlerService requestHandler) =>
    await requestHandler.GetStudentByIdAsync(id));
app.MapPost("/student/create",
    async (IFormCollection form, RequestHandlerService requestHandler) =>
    await requestHandler.CreateStudentAsync(form)).DisableAntiforgery();

app.Run();
