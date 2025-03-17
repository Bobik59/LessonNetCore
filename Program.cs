using LessonNetCore.Service.Infrastructure;
using LessonNetCore.Service.Interface;
using LessonNetCore.ServiceReader.Infrastructure;
using LessonNetCore.ServiceReader.Interface;
using LessonNetCore;

string filePath = "books.txt";

// Вывод в консоль
IOutputService consoleOutput = new ConsoleOutputService();
IBookReader bookReader = new FlexibleBookReader();
BookService bookService = new BookService(consoleOutput, bookReader);
bookService.LoadAndDisplayBooks(filePath);

// Вывод в файл
IOutputService fileOutput = new FileOutputService("output_books.txt");
BookService fileBookService = new BookService(fileOutput, bookReader);
fileBookService.LoadAndDisplayBooks(filePath);