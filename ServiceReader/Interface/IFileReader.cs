using LessonNetCore.Entity;

namespace LessonNetCore.ServiceReader.Interface
{
    public interface IFileReader
    {
        List<Person> ReadFromFile(string filePath);
    }
}
