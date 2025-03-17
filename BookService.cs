using LessonNetCore.Entity;
using LessonNetCore.Service.Interface;
using LessonNetCore.ServiceReader.Interface;

namespace LessonNetCore
{
    public class BookService
    {
        private readonly IOutputService _outputService;
        private readonly IBookReader _bookReader;

        public BookService(IOutputService outputService, IBookReader bookReader)
        {
            _outputService = outputService;
            _bookReader = bookReader;
        }

        public void DisplayBookInfo(Book book)
        {
            _outputService.Write(book.ToString());
        }

        public void LoadAndDisplayBooks(string filePath)
        {
            var books = _bookReader.ReadFromFile(filePath);
            foreach (var book in books)
            {
                DisplayBookInfo(book);
                _outputService.Write("\n");
            }
        }
    }

}
