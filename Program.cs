using LessonNetCore.Models;


List<Book> books = new List<Book>()
{
    new Book()
    {
    Title = "",
    Author = "",
    Description = "",
    },
    new Book()
    {
    Title = "",
    Author = "",
    Description = "",
    }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/books", (appBuilder) =>
{
    appBuilder.Run(async (context) =>
    {
        context.Response.ContentType = "text"
        await context.Response.SendFileAsync("HTML/index.html");
    });
});

app.Run();
