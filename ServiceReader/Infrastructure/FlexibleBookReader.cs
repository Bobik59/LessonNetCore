using LessonNetCore.Entity;
using LessonNetCore.ServiceReader.Interface;

namespace LessonNetCore.ServiceReader.Infrastructure
{
    public class FlexibleBookReader : IBookReader
    {
        private readonly int _separatorCount;

        public FlexibleBookReader(int separatorCount)
        {
            _separatorCount = separatorCount;
        }
        
        public List<Book> ReadFromFile(string filePath)
        {
            var books = new List<Book>();
            var lines = File.ReadAllLines(filePath).ToList();
            

            List<string> currentBookData = new List<string>();
            int emptyLineCount = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    emptyLineCount++;

                    if (emptyLineCount >= _separatorCount)
                    {
                        if (currentBookData.Count > 0)
                        {
                            var book = ParseBook(currentBookData);
                            if (book != null)
                                books.Add(book);
                        }
                        currentBookData.Clear(); 
                        emptyLineCount = 0;
                    }
                }
                else
                {
                    currentBookData.Add(line);
                }
            }

            if (currentBookData.Count > 0)
            {
                var book = ParseBook(currentBookData);
                if (book != null)
                    books.Add(book);
            }

            return books;
        }

        private Book ParseBook(List<string> bookData)
        {
            string title = "", author = "", genre = "", additionalInfo = "";
            int year = 0;

            foreach (var line in bookData)
            {
                if (line.StartsWith("Название книги:"))
                    title = line.Replace("Название книги:", "").Trim();
                else if (line.StartsWith("Автор:"))
                    author = line.Replace("Автор:", "").Trim();
                else if (line.StartsWith("Стиль:"))
                    genre = line.Replace("Стиль:", "").Trim();
                else if (line.StartsWith("Год издания:") && int.TryParse(line.Replace("Год издания:", "").Trim(), out int parsedYear))
                    year = parsedYear;
                else if (line.StartsWith("Доп. информация:"))
                    additionalInfo = line.Replace("Доп. информация:", "").Trim();
            }

            return new Book(title, author, genre, year, additionalInfo);
        }
    }

}
