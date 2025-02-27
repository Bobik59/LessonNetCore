namespace LessonNetCore.Models
{
    public class Book
    {
        public static int Count { get; set; }
        static Book()
        {
            Count = 1;
        }

        public Book()
        {
            Id = Count++;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

    }
}
