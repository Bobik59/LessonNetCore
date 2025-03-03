using static System.Net.Mime.MediaTypeNames;

namespace LessonNetCore
{
    interface ILogger
    {
        void Log(string message);
    }

    class LoggerConsole : ILogger
    {
        public void Log(string message) => Console.WriteLine(message);
    }

    class LoggerFile : ILogger
    {
        public void Log(string message)
        {
            string filePath = "persons.txt";

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine(message);
            }
        }
    }

    class Message
    {
        ILogger logger;
        public Message(ILogger logger)
        {
            this.logger = logger;
        }
        public void Print(Person person) => logger.Log(person.ToString());
    }

    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string surname { get; set; }
        public DateTime DateBirth { get; set; }
        public string information { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Surname: {surname}, DateBirth: {DateBirth:dd.MM.yyyy}, Information: {information}";
        }
    }
}
