using Microsoft.AspNetCore.Http;
using System;

void Example3()
{
    var builder = WebApplication.CreateBuilder();
    var app = builder.Build();

    var books = new List<Book>
        {
            new Book { Id = 1, Title = "1984", Author = "George Orwell", Year = 1949, Genre = "Dystopian, Political fiction", Isbn = "978-0451524935" },
            new Book { Id = 2, Title = "Brave New World", Author = "Aldous Huxley", Year = 1932, Genre = "Dystopian, Science fiction", Isbn = "978-0060850524" },
            new Book { Id = 3, Title = "To Kill a Mockingbird", Author = "Harper Lee", Year = 1960, Genre = "Southern Gothic, Drama", Isbn = "978-0061120084" },
            new Book { Id = 4, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Year = 1951, Genre = "Fiction, Coming-of-age", Isbn = "978-0316769488" },
            new Book { Id = 5, Title = "Pride and Prejudice", Author = "Jane Austen", Year = 1813, Genre = "Romance, Satire", Isbn = "978-1503290563" },
            new Book { Id = 6, Title = "Moby-Dick", Author = "Herman Melville", Year = 1851, Genre = "Adventure, Epic", Isbn = "978-1503280786" },
            new Book { Id = 7, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Year = 1925, Genre = "Tragedy, Novel", Isbn = "978-0743273565" },
            new Book { Id = 8, Title = "The Hobbit", Author = "J.R.R. Tolkien", Year = 1937, Genre = "Fantasy, Adventure", Isbn = "978-0618968633" },
            new Book { Id = 9, Title = "War and Peace", Author = "Leo Tolstoy", Year = 1869, Genre = "Historical fiction", Isbn = "978-1400079988" },
            new Book { Id = 10, Title = "The Brothers Karamazov", Author = "Fyodor Dostoevsky", Year = 1880, Genre = "Philosophical, Fiction", Isbn = "978-0374528379" },
            new Book { Id = 11, Title = "Crime and Punishment", Author = "Fyodor Dostoevsky", Year = 1866, Genre = "Psychological fiction", Isbn = "978-0486415871" },
            new Book { Id = 12, Title = "Les Misérables", Author = "Victor Hugo", Year = 1862, Genre = "Historical fiction", Isbn = "978-0451419439" },
            new Book { Id = 13, Title = "The Picture of Dorian Gray", Author = "Oscar Wilde", Year = 1890, Genre = "Philosophical fiction, Gothic", Isbn = "978-0486266846" },
            new Book { Id = 14, Title = "The Odyssey", Author = "Homer", Year = 8, Genre = "Epic, Adventure", Isbn = "978-0140268867" },
            new Book { Id = 15, Title = "Dracula", Author = "Bram Stoker", Year = 1897, Genre = "Gothic horror", Isbn = "978-0486411095" },
            new Book { Id = 16, Title = "Frankenstein", Author = "Mary Shelley", Year = 1818, Genre = "Gothic horror, Science fiction", Isbn = "978-0486282112" },
            new Book { Id = 17, Title = "Alice's Adventures in Wonderland", Author = "Lewis Carroll", Year = 1865, Genre = "Fantasy, Adventure", Isbn = "978-0486275435" },
            new Book { Id = 18, Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Year = 1954, Genre = "Fantasy, Adventure", Isbn = "978-0618640157" },
            new Book { Id = 19, Title = "Fahrenheit 451", Author = "Ray Bradbury", Year = 1953, Genre = "Dystopian, Science fiction", Isbn = "978-1451673319" },
            new Book { Id = 20, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Year = 1951, Genre = "Fiction, Coming-of-age", Isbn = "978-0316769488" }
        };

    app.Map("/home", appBuilder =>
    {
        appBuilder.Map("/books", Books);


        appBuilder.Run(async (context) => await context.Response.WriteAsync("Home Page"));
    });

    app.Run();

    void Books(IApplicationBuilder appBuilder)
    {
        appBuilder.Run(async (context) => await context.Response.SendFileAsync("index.html"));
    }
}

//    void Book(IApplicationBuilder appBuilder)
//    {
//        appBuilder.UseWhen(
//            (context) =>
//            {
//                return int.TryParse(context.Request.Path.ToString().Split("/").Last(), out _);
//            },
//            (app) =>
//            {
//                int? ID = null;
//                appBuilder.Use(async (context, next) =>
//                {
//                    ID = Convert.ToInt32(context.Request.Path.ToString().Split("/").Last());

//                    var book = books.Find(b => b.Id == ID);
//                    if (book != null)
//                    {
                        
//                    }
//                    else
//                    {
//                        context.Response.StatusCode = 404;
//                        await context.Response.WriteAsync("<html><body><h1>Book not found</h1></body></html>");
//                    }
//                    await next();
//                });
//            }
//        );
//    }

//}
Example3();
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }
    public string Isbn { get; set; }
}