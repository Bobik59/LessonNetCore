using LessonNetCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/", () => "Hello World!");

using (var db = new DbGameStoreContext())
{
    db.Database.EnsureCreated();  // Создает базу данных, если она еще не существует
}


app.UseStaticFiles();

app.Map("/api/gamesForDelete", async context =>
{
    using var db = new DbGameStoreContext();
    var games = await db.Games
        .Select(g => new { g.Id, g.Name, g.Author })
        .ToListAsync();
    await context.Response.WriteAsJsonAsync(games);
});

app.Map("/api/delete/{id:int}", async (int id, HttpContext context) =>
{
    using var db = new DbGameStoreContext();
    var game = await db.Games.FindAsync(id);
    if (game != null)
    {
        db.Games.Remove(game);
        await db.SaveChangesAsync();
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("Game deleted");
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Game not found");
    }
});

app.Map("/Delete", async context =>
{
    var env = app.Services.GetRequiredService<IWebHostEnvironment>();
    var webRootPath = env.WebRootPath;

    if (string.IsNullOrEmpty(webRootPath))
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync($"WebRootPath is not set.");
        return;
    }

    var filePath = Path.Combine(webRootPath, "delete.html");

    if (!File.Exists(filePath))
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync($"File not found. Path: {filePath}");
        return;
    }

    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync(filePath);
});


app.Map("/api/Game/{id:int}", async (int id, HttpContext context) =>
{
    using var db = new DbGameStoreContext();
    var game = await db.Games.FindAsync(id);

    if (game == null)
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Game not found");
        return;
    }

    // Отправляем информацию об игре в формате JSON
    await context.Response.WriteAsJsonAsync(new
    {
        game.Name,
        game.Genre,
        game.Author,
        game.PicturePath
    });
});

app.Map("/Game/{id:int}", async (int id, HttpContext context) =>
{
    using var db = new DbGameStoreContext();
    var game = await db.Games.FindAsync(id);

    if (game == null)
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Game not found");
        return;
    }

    // Читаем шаблон HTML из файла
    var htmlTemplate = await File.ReadAllTextAsync("wwwroot/gameDetailTemplate.html");

    // Заменяем плейсхолдеры на реальные данные
    var htmlContent = htmlTemplate
        .Replace("{gameName}", game.Name)
        .Replace("{gameGenre}", game.Genre)
        .Replace("{gameAuthor}", game.Author)
        .Replace("{gamePicturePath}", game.PicturePath);

    // Отправляем сгенерированный HTML
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(htmlContent);
});




app.Map("/api/games", async context =>
{
    using var db = new DbGameStoreContext();
    var games = db.Games.Select(g => new
    {
        g.Name,
        g.Genre,
        g.Author,
        g.PicturePath
    }).ToList();

    await context.Response.WriteAsJsonAsync(games);
});

app.Map("/Games", async context =>
{
    var env = app.Services.GetRequiredService<IWebHostEnvironment>();
    var webRootPath = env.WebRootPath;

    if (string.IsNullOrEmpty(webRootPath))
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync($"WebRootPath is not set.");
        return;
    }

    var filePath = Path.Combine(webRootPath, "games.html");

    if (!File.Exists(filePath))
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync($"File not found. Path: {filePath}");
        return;
    }

    context.Response.ContentType = "text/html";  // Устанавливаем правильный MIME-тип
    await context.Response.SendFileAsync(filePath);  // Отправляем HTML файл
});


app.Map("/Games/Create", async (context) =>
{
    var author = context.Request.Query["author"];
    var genre = context.Request.Query["genre"];
    var name = context.Request.Query["name"];
    var picturePath = context.Request.Query["picturePath"];

    if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(genre) || string.IsNullOrEmpty(name))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Missing required parameters.");
        return;
    }

    using var db = new DbGameStoreContext();
    var game = new Game
    {
        Author = author,
        Genre = genre,
        Name = name,
        PicturePath = picturePath,
    };

    db.Games.Add(game);
    await db.SaveChangesAsync();

    context.Response.StatusCode = 201;
    await context.Response.WriteAsync("Game created successfully.");
});

app.Run();
