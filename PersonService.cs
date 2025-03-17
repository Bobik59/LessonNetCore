using LessonNetCore.Service.Interface;
using LessonNetCore.ServiceReader.Interface;

namespace LessonNetCore
{
    public class PersonService
    {
        private readonly IFileReader _fileReader;
        private readonly IOutputService _outputService;

        public PersonService(IFileReader fileReader, IOutputService outputService)
        {
            _fileReader = fileReader;
            _outputService = outputService;
        }

        public void LoadAndPrintPersons(string filePath)
        {
            var persons = _fileReader.ReadFromFile(filePath);
            foreach (var person in persons)
            {
                _outputService.Write(person.ToString());
            }
        }
    }

}
