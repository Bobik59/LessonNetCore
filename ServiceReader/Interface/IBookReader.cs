using LessonNetCore.Entity;

namespace LessonNetCore.ServiceReader.Interface
{
    public interface IBookReader
    {
        List<Book> ReadFromFile(string filePath);
    }
}
