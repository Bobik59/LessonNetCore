using LessonNetCore.Entity;
using LessonNetCore.ServiceReader.Interface;

namespace LessonNetCore.ServiceReader.Infrastructure
{
    public class AsteriskFileReader : IFileReader
    {
        public List<Person> ReadFromFile(string filePath)
        {
            var persons = new List<Person>();
            var lines = File.ReadAllLines(filePath);

            Person currentPerson = null;
            foreach (var line in lines)
            {
                if (line.StartsWith("*"))
                {
                    if (currentPerson != null)
                        persons.Add(currentPerson);

                    currentPerson = new Person("", "", DateTime.MinValue, "");
                }
                else if (currentPerson != null)
                {
                    if (line.StartsWith("Имя:"))
                        currentPerson = new Person(line.Replace("Имя:", "").Trim(), currentPerson.LastName, currentPerson.DateOfBirth, currentPerson.OtherInfo);
                    else if (line.StartsWith("Фамилия:"))
                        currentPerson = new Person(currentPerson.FirstName, line.Replace("Фамилия:", "").Trim(), currentPerson.DateOfBirth, currentPerson.OtherInfo);
                    else if (line.StartsWith("Дата рождения:"))
                    {
                        if (DateTime.TryParse(line.Replace("Дата рождения:", "").Trim(), out DateTime date))
                            currentPerson = new Person(currentPerson.FirstName, currentPerson.LastName, date, currentPerson.OtherInfo);
                    }
                    else if (line.StartsWith("Доп. информация:"))
                        currentPerson = new Person(currentPerson.FirstName, currentPerson.LastName, currentPerson.DateOfBirth, line.Replace("Доп. информация:", "").Trim());
                }
            }

            if (currentPerson != null)
                persons.Add(currentPerson);

            return persons;
        }
    }


    public class PercentFileReader : IFileReader
    {
        public List<Person> ReadFromFile(string filePath)
        {
            var persons = new List<Person>();
            var lines = File.ReadAllLines(filePath);

            Person currentPerson = null;
            foreach (var line in lines)
            {
                if (line.StartsWith("%"))
                {
                    if (currentPerson != null)
                        persons.Add(currentPerson);

                    currentPerson = new Person("", "", DateTime.MinValue, "");
                }
                else if (currentPerson != null)
                {
                    if (line.StartsWith("Имя:"))
                        currentPerson = new Person(line.Replace("Имя:", "").Trim(), currentPerson.LastName, currentPerson.DateOfBirth, currentPerson.OtherInfo);
                    else if (line.StartsWith("Фамилия:"))
                        currentPerson = new Person(currentPerson.FirstName, line.Replace("Фамилия:", "").Trim(), currentPerson.DateOfBirth, currentPerson.OtherInfo);
                    else if (line.StartsWith("Дата рождения:"))
                    {
                        if (DateTime.TryParse(line.Replace("Дата рождения:", "").Trim(), out DateTime date))
                            currentPerson = new Person(currentPerson.FirstName, currentPerson.LastName, date, currentPerson.OtherInfo);
                    }
                    else if (line.StartsWith("Доп. информация:"))
                        currentPerson = new Person(currentPerson.FirstName, currentPerson.LastName, currentPerson.DateOfBirth, line.Replace("Доп. информация:", "").Trim());
                }
            }

            if (currentPerson != null)
                persons.Add(currentPerson);

            return persons;
        }
    }
}
