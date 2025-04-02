using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LessonNetCore.Data;
using LessonNetCore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        "Server=(localdb)\\MSSQLLocalDB;Database=GameDb;Trusted_Connection=True;",
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        )
    )
);

var app = builder.Build();

app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    if (!db.Games.Any())
    {
        db.Games.AddRange(new List<Game>
        {
            new Game { Name = "Battle Quest", Genre = "Action", Author = "AAA", ImagePath = "images/game1.jpg" },
            new Game { Name = "Mystery Island", Genre = "Adventure", Author = "BBB", ImagePath = "images/game2.jpg" }
        });
        db.SaveChanges();
    }
}

app.MapGet("/games", (IWebHostEnvironment env) =>
{
    string path = Path.Combine(env.WebRootPath, "html", "games.html");
    return Results.File(path, "text/html");
});

app.MapGet("/api/games", async (HttpRequest request, ApplicationDbContext db) =>
{
    var query = db.Games.AsQueryable();

    string? author = request.Query["author"];
    string? genre = request.Query["genre"];

    if (!string.IsNullOrEmpty(author))
        query = query.Where(g => g.Author.Contains(author));

    if (!string.IsNullOrEmpty(genre))
        query = query.Where(g => g.Genre.Contains(genre));

    var games = await query.ToListAsync();
    return Results.Json(games);
});

app.MapPut("/api/game/{id:int}/update", async (HttpContext context, int id, ApplicationDbContext db) =>
{
    var game = await db.Games.FindAsync(id);
    if (game == null) return Results.NotFound();

    var form = await context.Request.ReadFormAsync();
    game.Name = form["name"].ToString();
    game.Genre = form["genre"].ToString();
    game.Author = form["author"].ToString();
    game.Description = form["description"].ToString();

    var file = form.Files["image"];
    if (file != null && file.Length > 0)
    {
        var uploads = Path.Combine(context.RequestServices.GetRequiredService<IWebHostEnvironment>().WebRootPath, "images");
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploads, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        game.ImagePath = "images/" + fileName;
    }

    await db.SaveChangesAsync();
    return Results.Json(game);
});


app.MapDelete("/api/game/{id:int}/delete", async (int id, ApplicationDbContext db) =>
{
    var game = await db.Games.FindAsync(id);
    if (game == null) return Results.NotFound();

    db.Games.Remove(game);
    await db.SaveChangesAsync();
    return Results.Ok();
});


app.MapGet("/api/game/{id:int}", async (int id, ApplicationDbContext db) =>
{
    var game = await db.Games.FindAsync(id);
    return game != null ? Results.Json(game) : Results.NotFound();
});

app.MapGet("/game/create", (IWebHostEnvironment env) =>
{
    string path = Path.Combine(env.WebRootPath, "html", "create.html");
    return Results.File(path, "text/html; charset=UTF-8");
});

app.MapPost("/api/game/create", async (HttpContext context, ApplicationDbContext db) =>
{
    var form = await context.Request.ReadFormAsync();

    var name = form["name"].ToString();
    var genre = form["genre"].ToString();
    var author = form["author"].ToString();
    var description = form["description"].ToString();
    var file = form.Files["image"];

    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(genre) || string.IsNullOrWhiteSpace(author))
        return Results.BadRequest("Заполните все поля");

    // Обработка загрузки файла
    string imagePath = "images/default.jpg";
    if (file != null && file.Length > 0)
    {
        var uploads = Path.Combine(context.RequestServices.GetRequiredService<IWebHostEnvironment>().WebRootPath, "images");
        Directory.CreateDirectory(uploads); // Создадим папку, если её нет
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploads, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        imagePath = "images/" + fileName;
    }

    // Создаем игру
    var game = new Game
    {
        Name = name,
        Genre = genre,
        Author = author,
        Description = string.IsNullOrWhiteSpace(description) ? "Нет описания." : description,
        ImagePath = imagePath
    };

    db.Games.Add(game);
    await db.SaveChangesAsync();

    return Results.Json(game);
});


app.MapGet("/game/image/{id:int}", async (int id, ApplicationDbContext db) =>
{
    var game = await db.Games.FindAsync(id);
    if (game == null)
        return Results.NotFound();

    var filePath = Path.Combine(app.Environment.WebRootPath, game.ImagePath);
    if (!System.IO.File.Exists(filePath))
        return Results.NotFound();

    return Results.File(filePath, "image/jpeg");
});

app.MapPost("/game/{name}/{genre}/{author}", async (string name, string genre, string author, ApplicationDbContext db) =>
{
    var game = new Game
    {
        Name = name,
        Genre = genre,
        Author = author,
        ImagePath = "images/default.jpg"
    };
    db.Games.Add(game);
    await db.SaveChangesAsync();
    return Results.Json(game);
});

app.MapPut("/game/{id:int}/{name}/{genre}/{author}", async (int id, string name, string genre, string author, ApplicationDbContext db) =>
{
    var game = await db.Games.FindAsync(id);
    if (game == null)
        return Results.NotFound();

    game.Name = name;
    game.Genre = genre;
    game.Author = author;

    await db.SaveChangesAsync();
    return Results.Json(game);
});

app.MapDelete("/game/{id:int}", async (int id, ApplicationDbContext db) =>
{
    var game = await db.Games.FindAsync(id);
    if (game == null)
        return Results.NotFound();

    db.Games.Remove(game);
    await db.SaveChangesAsync();
    return Results.Json(game);
});

app.Run();