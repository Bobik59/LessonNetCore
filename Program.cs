////������� ����������
//void Example1()
//{
//    var builder = WebApplication.CreateBuilder(args);
//    var app = builder.Build();
//    //��������
//    //Configuration: ������������ ������������ ���������� � ���� ������� IConfiguration
//    //Environment: ������������ ��������� ���������� � ���� IWebHostEnvironment
//    //Lifetime: ��������� �������� ����������� � �������� ���������� ����� ����������
//    //Logger: ������������ ������ ���������� �� ���������
//    //Services: ������������ ������� ����������
//    //Urls: ������������ ����� �������, ������� ���������� ������

//    app.MapGet("/", () => "Hello World!");
//    //���������� middleware ������������ � ������� ������� ���������� Run, Map � Use ���������� IApplicationBuilder
//    //app.UseWelcomePage();

//    app.Run();
//}

////����� Run � ������ HttpContext
//void Example2()
//{
//    //�������� middleware
//    //public delegate Task RequestDelegate(HttpContext context);
//    //�������� HttpContext
//    //Connection: ������������ ���������� � �����������, ������� ����������� ��� ������� �������
//    //Features: �������� ��������� HTTP-�����������������, ������� �������� ��� ����� �������
//    //Items: �������� ��� ������������� ��������� ��� ����-�������� ��� �������� ��������� ������ ��� �������� �������
//    //Request: ���������� ������ HttpRequest, ������� ������ ���������� � ������� �������
//    //RequestAborted: ���������� ����������, ����� ����������� �����������, � �������������� ��������� ������� ������ ���� ��������
//    //RequestServices: �������� ��� ������������� ������ IServiceProvider, ������� ������������� ������ � ���������� �������� �������
//    //Response: ���������� ������ HttpResponse, ������� ��������� ��������� ������� �������
//    //Session: ������ ������ ������ ��� �������� �������
//    //TraceIdentifier: ������������ ���������� ������������� ������� ��� ����� �����������
//    //User: ������������ ������������, ���������������� � ���� ��������
//    //WebSockets: ���������� ������ ��� ���������� ������������� WebSocket ��� ������� �������

//    var builder = WebApplication.CreateBuilder();

//    var app = builder.Build();

//    app.Run(async (context) => await context.Response.WriteAsync(DateTime.Now.ToLongDateString()));
//    //app.Run(HandleRequst);

//    app.Run();

//    async Task HandleRequst(HttpContext context)
//    {
//        await context.Response.WriteAsync(DateTime.Now.ToLongDateString());
//    }
//}

////Response
//void Example3()
//{
//    //�������� Response

//    //Body: �������� ��� ������������� ���� ������ � ���� ������� Stream
//    //BodyWriter: ���������� ������ ���� PipeWriter ��� ������ ������
//    //ContentLength: �������� ��� ������������� ��������� Content-Length
//    //ContentType: �������� ��� ������������� ��������� Content-Type
//    //Cookies: ���������� ����, ������������ � ������
//    //HasStarted: ���������� true, ���� �������� ������ ��� ��������
//    //Headers: ���������� ��������� ������
//    //Host: �������� ��� ������������� ��������� Host
//    //HttpContext: ���������� ������ HttpContext, ��������� � ������ �������� Response
//    //StatusCode: ���������� ��� ������������� ��������� ��� ������

//    var builder = WebApplication.CreateBuilder();
//    var app = builder.Build();

//    app.Run(HandleRequst);
//    //app.Run(HandleRequstHtml);

//    app.Run();

//    async Task HandleRequst(HttpContext context)
//    {
//        var response = context.Response;
//        response.Headers.ContentLanguage = "ru-RU";
//        response.Headers.ContentType = "text/plain; charset=utf-8";

//        response.StatusCode = 200;

//        await response.WriteAsync(DateTime.Now.ToLongDateString());
//    }
//    //�������� html
//    async Task HandleRequstHtml(HttpContext context)
//    {
//        var response = context.Response;
//        response.ContentType = "text/html; charset=utf-8";

//        await response.WriteAsync("<h1>Hello world!</h1>");
//    }
//}

////Request
//void Example4()
//{
//    //�������� Request
//    //Body: ������������� ���� ������� � ���� ������� Stream
//    //BodyReader: ���������� ������ ���� PipeReader ��� ������ ���� �������
//    //ContentLength: �������� ��� ������������� ��������� Content-Length
//    //ContentType: �������� ��� ������������� ��������� Content-Type
//    //Cookies: ���������� ��������� ���� (������ Cookies), ��������������� � ������ ��������
//    //Form: �������� ��� ������������� ���� ������� � ���� ����
//    //HasFormContentType: ��������� ������� ��������� Content-Type
//    //Headers: ���������� ��������� �������
//    //Host: �������� ��� ������������� ��������� Host
//    //HttpContext: ���������� ��������� � ������ �������� ������ HttpContext
//    //IsHttps: ���������� true, ���� ����������� �������� https
//    //Method: �������� ��� ������������� ����� HTTP
//    //Path: �������� ��� ������������� ���� ������� � ���� ������� RequestPath
//    //PathBase: �������� ��� ������������� ������� ���� �������. ����� ���� �� ������ ��������� ����������� ����
//    //Protocol: �������� ��� ������������� ��������, ��������, HTTP
//    //Query: ���������� ��������� ���������� �� ������ �������
//    //QueryString: �������� ��� ������������� ������ �������
//    //RouteValues: �������� ������ �������� ��� �������� �������
//    //Scheme: �������� ��� ������������� ����� ������� HTTP

//    var builder = WebApplication.CreateBuilder();
//    var app = builder.Build();
//    app.Run(HandleRequstHeaders);
//    app.Run();

//    async Task HandleRequstHeaders(HttpContext context)
//    {
//        context.Response.ContentType = "text/html; charset=utf-8";
//        var stringBuilder = new System.Text.StringBuilder("<table>");

//        foreach (var header in context.Request.Headers)
//        {
//            stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
//        }
//        stringBuilder.Append("</table>");
//        await context.Response.WriteAsync(stringBuilder.ToString());
//    }
//    async Task HandleRequstPath(HttpContext context)
//    {
//        var path = context.Request.Path;
//        var response = context.Response;

//        if (path == "/date")
//            await response.WriteAsync($"Date: {DateTime.Now.ToShortDateString()}");
//        else if (path == "/time")
//            await response.WriteAsync($"Time: {DateTime.Now.ToShortTimeString()}");
//        else
//            await response.WriteAsync("�������� �� �������.");
//    }
//    //������ �������
//}

////��������� �����
void Example5()
{
    var builder = WebApplication.CreateBuilder();
    var app = builder.Build();

    app.Run(HandleRequst);
    app.Run();

    async Task HandleRequst(HttpContext context)
    {
        context.Response.ContentType = "text/html; charset=utf-8";

        if (context.Request.Path == "/postuser")
        {
            var form = context.Request.Form;
            string name = form["name"];
            string age = form["age"];
            await context.Response.WriteAsync($"<div><p>Name: {name}</p><p>Age: {age}</p></div>");
        }
        else
        {
            await context.Response.SendFileAsync("html/Example5.html");
        }
    }
}

////������ � Json
////void Example6()
////{
////    var builder = WebApplication.CreateBuilder();
////    var app = builder.Build();

////    app.Run(HandleRequst);
////    app.Run();

////    async Task HandleRequst(HttpContext context)
////    {
////        var response = context.Response;
////        var request = context.Request;
////        if (request.Path == "/api/user")
////        {
////            var message = "������������ ������";   // ���������� ��������� �� ���������
////            try
////            {
////                // �������� �������� ������ json
////                var person = await request.ReadFromJsonAsync<Person>();
////                if (person != null) // ���� ������ ��������������� � Person
////                    message = $"Name: {person.Name}  Age: {person.Age}";
////            }
////            catch
////            {
////                app.Logger.LogError("������ ������ ������.");
////            }
////            // ���������� ������������ ������
////            await response.WriteAsJsonAsync(new { text = message });
////        }
////        else
////        {
////            response.ContentType = "text/html; charset=utf-8";
////            await response.SendFileAsync("html/Example6.html");
////        }
////    }
////}

////����� Use
//void Example7()
//{
//    var builder = WebApplication.CreateBuilder();
//    var app = builder.Build();

//    string date = "";

//    app.Use(async (context, next) =>
//    {
//        date = DateTime.Now.ToShortDateString();
//        await next.Invoke();
//        Console.WriteLine($"Current date: {date}");
//    });
//    //app.Use(async (context, next) =>
//    //{
//    //    date = DateTime.Now.ToShortDateString();
//    //    await next.Invoke(context);                 // ����� next - RequestDelegate
//    //    Console.WriteLine($"Current date: {date}");
//    //});

//    app.Run(async (context) => await context.Response.WriteAsync($"Date: {date}"));
//    app.Run();


//    async Task GetDateFunc(HttpContext context, Func<Task> next)
//    {
//        date = DateTime.Now.ToShortDateString();
//        await next.Invoke();
//        Console.WriteLine($"Current date: {date}");
//    }
//    async Task GetDateRequestDelegate(HttpContext context, RequestDelegate next)
//    {
//        date = DateTime.Now.ToShortDateString();
//        await next.Invoke(context);
//        Console.WriteLine($"Current date: {date}");
//    }
//    async Task GetDateRequestTerminal(HttpContext context, RequestDelegate next)
//    {
//        string path = context.Request.Path;
//        if (path == "/date")
//        {
//            await context.Response.WriteAsync($"Date: {DateTime.Now.ToShortDateString()}");
//        }
//        else
//        {
//            await next.Invoke(context);
//        }
//    }
//}

////����� UseWhen
//void Example8()
//{
//    //public static IApplicationBuilder UseWhen (this IApplicationBuilder app, Func<HttpContext,bool> predicate,
//    //  Action<IApplicationBuilder> configuration);
//    //������� Func<HttpContext,bool> - ��������� �������, �������� ������ ��������������� ������
//    //������� Action<IApplicationBuilder> ������������ ��������� �������� ��� �������� IApplicationBuilder

//    var builder = WebApplication.CreateBuilder();
//    var app = builder.Build();

//    app.UseWhen(
//        context => context.Request.Path == "/time", // ���� ���� ������� "/time"
//        appBuilder =>
//        {
//            string time = null;
//            // ��������� ������ - ������� �� ������� ����������
//            appBuilder.Use(async (context, next) =>
//            {
//                time = DateTime.Now.ToShortTimeString();
//                Console.WriteLine($"Time: {time}");
//                await next();   // �������� ��������� middleware
//            });

//            // ���������� �����
//            appBuilder.Run(async context =>
//            {
//                await context.Response.WriteAsync($"Time: {time}");
//            });
//        });

//    app.Run(async context =>
//    {
//        await context.Response.WriteAsync("Home page");
//    });

//    app.Run();
//}
////����� Map
//void Example9()
//{
//    var builder = WebApplication.CreateBuilder();
//    var app = builder.Build();

//    app.Map("/time", appBuilder =>
//    {
//        var time = DateTime.Now.ToShortTimeString();

//        appBuilder.Use(async (context, next) =>
//        {
//            Console.WriteLine($"Time: {time}");
//            await next();
//        });

//        appBuilder.Run(async context => await context.Response.WriteAsync($"Time: {time}"));
//    });

//    app.Run(async (context) => await context.Response.WriteAsync("Hello world!"));

//    app.Run();
//}

////��������� Map
//void Example10()
//{
//    var builder = WebApplication.CreateBuilder();
//    var app = builder.Build();

//    app.Map("/home", appBuilder =>
//    {
//        appBuilder.Map("/index", Index);
//        appBuilder.Map("/about", About);

//        appBuilder.Run(async (context) => await context.Response.WriteAsync("Home Page"));
//    });

//    app.Run(async (context) => await context.Response.WriteAsync("Page Not Found"));

//    app.Run();

//    void Index(IApplicationBuilder appBuilder)
//    {
//        appBuilder.Run(async context => await context.Response.WriteAsync("Index Page"));
//    }
//    void About(IApplicationBuilder appBuilder)
//    {
//        appBuilder.Run(async context => await context.Response.WriteAsync("About Page"));
//    }
//}



//public record Person(string Name, int Age);

