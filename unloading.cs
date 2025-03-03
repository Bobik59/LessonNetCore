using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Xml.Linq;

namespace LessonNetCore
{
    interface IExtractor
    {
        List<Person> Extract(string Text);
    }

    class ExtractFromJson : IExtractor
    {
        public List<Person> Extract(string Text) 
        {
            List<Person> persons = new List<Person>()
            {
                new Person(){ Id = 1, Name = "Name", surname = "surName", DateBirth = new DateTime(2025, 03, 21), information = "Info" },
                new Person(){ Id = 2, Name = "Name2", surname = "surName2", DateBirth = new DateTime(2025, 03, 21), information = "Info2" },

            };
            return persons;
        }
    }

    class FileData 
    {
        IExtractor extractor;
        public FileData(IExtractor extractor)
        {
            this.extractor = extractor;
        }
        public List<Person> FileUnloud(string path)
        {
            string fileText = File.ReadAllText(path);

            return extractor.Extract(fileText);
        }
    }

}
