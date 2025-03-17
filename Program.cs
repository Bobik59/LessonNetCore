using LessonNetCore.Service.Infrastructure;
using LessonNetCore.Service.Interface;
using LessonNetCore.ServiceReader.Infrastructure;
using LessonNetCore.ServiceReader.Interface;
using LessonNetCore;
using LessonNetCore.Entity;

string filePath = "people_info.txt";

IFileReader fileReader = new PercentFileReader();


IOutputService consoleService = new ConsoleOutputService();
PersonService personService = new PersonService(fileReader, consoleService);

personService.LoadAndPrintPersons(filePath);

List<Person> persons = fileReader.ReadFromFile(filePath);/
foreach (var person in persons)
{
    consoleService.Write(person.ToString());
}