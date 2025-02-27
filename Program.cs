using LessonNetCore.Models;
using System;

List<Book> books = new List<Book>()
{
    new Book() { Title = "����� 1", Author = "����� 1", Description = "�������� 1" },
    new Book() { Title = "����� 2", Author = "����� 2", Description = "�������� 2" }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ������ HTML-�������� �� ������� ����
app.Map("/books", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("HTML/index.html");
    });
});

// API: ������ ����
app.Map("/api/books", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        await context.Response.WriteAsJsonAsync(books);
    });
});

app.MapPost("/books/add", async (HttpContext context) =>
{
    var newBook = await context.Request.ReadFromJsonAsync<Book>();
    if (newBook == null)
    {
        return Results.BadRequest("�������� ������");
    }

    newBook.Id = books.Any() ? books.Max(b => b.Id) + 1 : 1;
    books.Add(newBook);

    // �������� �� �������� ������������� ��� �� �������� ����������
    context.Response.Redirect("/Add.html");
    return Results.Empty;
});




// API: ��������� ���������� ����� �� Id
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

// ������� ��� ��������, ������������ ������ ���������� �����
app.Map("/home/{id:int}", async context =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("HTML/book.html");
});

app.Run();
