using LessonNetCore.Models;
using System.Reflection.PortableExecutable;


List<Book> books = new List<Book>()
{
    new Book()
    {
    Title = "ff",
    Author = "ff",
    Description = "ff",
    },
    new Book()
    {
    Title = "ff",
    Author = "ff",
    Description = "ff",
    }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/books", (appBuilder) =>
{
    appBuilder.Run(async (context) =>
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("HTML/index.html");
    });
});

app.Map("/api", (appBulder) =>
{
    appBulder.Map("/books", (appBuilder) =>
    {
        appBuilder.Run(async (context) =>
        {
            await context.Response.WriteAsJsonAsync(books);
        });
    });
});

app.UseWhen(
            (context) =>
            {
                return int.TryParse(context.Request.Path.ToString().Split("/").Last(), out _);
            },
            (app) =>
            {
                int? ID = null;
                app.Use(async (context, next) =>
                {
                    ID = Convert.ToInt32(context.Request.Path.ToString().Split("/").Last());

                    var book = books.Find(b => b.Id == ID);
                    if (book != null)
                    {
                        app.Run(async (context) =>
                        {
                            await context.Response.WriteAsJsonAsync(book);
                        });
                    }
                    await next();
                });
            }
        );

app.Run();
