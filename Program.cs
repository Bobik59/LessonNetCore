using LessonNetCore.Models;
using System;
using System.Reflection.PortableExecutable;

List<Book> books = new List<Book>()
{
    new Book() { Title = "Книга 1", Author = "Автор 1", Description = "Описание 1" },
    new Book() { Title = "Книга 2", Author = "Автор 2", Description = "Описание 2" }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/books", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("HTML/index.html");
    });
});


app.Map("/Add", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("HTML/Add.html");
    });
});

app.MapPost("/api/add", (Book book) =>
{
    // Добавляем книгу в список
    book.Id = Book.Count++;  // Пример логики для инкрементации ID
    books.Add(book);  // Добавление книги в список
    return Results.Ok(book);  // Возвращаем добавленную книгу
});

app.Map("/api/books", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        await context.Response.WriteAsJsonAsync(books);
    });
});

app.Map("/books/add", async (HttpContext context) =>
{
    var newBook = await context.Request.ReadFromJsonAsync<Book>();
    if (newBook == null)
    {
        return Results.BadRequest("Неверные данные");
    }

    newBook.Id = books.Any() ? books.Max(b => b.Id) + 1 : 1;
    books.Add(newBook);

    context.Response.Redirect("/Add.html");
    return Results.Empty;
});

app.Map("/api/book/{id:int}", async context =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    if (int.TryParse(context.Request.RouteValues["id"]?.ToString(), out int id))
    {
        var book = books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            await context.Response.WriteAsJsonAsync(book);
            return;
        }
    }
    context.Response.StatusCode = 404;
});

app.Map("/home/{id:int}", async context =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("HTML/book.html");
});

app.Run();
