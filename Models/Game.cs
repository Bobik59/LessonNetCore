namespace LessonNetCore.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ImagePath { get; set; } = "images/default.jpg";
        public string Description { get; set; } = "Нет описания.";
    }
}
