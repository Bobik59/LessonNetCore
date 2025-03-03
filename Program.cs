using LessonNetCore;
using Microsoft.VisualBasic;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


// Создаем экземпляр Message, передавая логгер в конструктор
Message message = new Message(new LoggerFile());
FileData fileData = new FileData(new ExtractFromJson());
List<Person> pers = fileData.FileUnloud("person.txt");
Person person = new Person() { Id = 1, Name = "Name", surname = "surName", DateBirth = new DateTime(2025, 03, 21), information = "Info" };
message.Print(person);
app.Run();
