using LessonNetCore.Models;
using System;
using System.Reflection.PortableExecutable;

List<Book> books = new List<Book>()
{
    new Book() { Title = "����� 1", Author = "����� 1", Description = "�������� 1" },
    new Book() { Title = "����� 2", Author = "����� 2", Description = "�������� 2" }
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
    // ��������� ����� � ������
    book.Id = Book.Count++;  // ������ ������ ��� ������������� ID
    books.Add(book);  // ���������� ����� � ������
    return Results.Ok(book);  // ���������� ����������� �����
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
        return Results.BadRequest("�������� ������");
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
