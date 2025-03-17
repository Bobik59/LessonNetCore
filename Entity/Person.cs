namespace LessonNetCore.Entity
{
    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public string Genre { get; }
        public int Year { get; }
        public string AdditionalInfo { get; }

        public Book(string title, string author, string genre, int year, string additionalInfo = "")
        {
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            AdditionalInfo = additionalInfo;
        }

        public override string ToString()
        {
            return $"Название книги: {Title}\nАвтор: {Author}\nСтиль: {Genre}\nГод издания: {Year}\nДоп. информация: {AdditionalInfo}";
        }
    }

}
