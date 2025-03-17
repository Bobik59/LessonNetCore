using LessonNetCore.Service.Interface;

namespace LessonNetCore.Service.Infrastructure
{
    public class ConsoleOutputService : IOutputService
    {
        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }

    public class FileOutputService : IOutputService
    {
        private readonly string _filePath;

        public FileOutputService(string filePath)
        {
            _filePath = filePath;
        }

        public void Write(string content)
        {
            File.WriteAllText(_filePath, content);
        }
    }

}
