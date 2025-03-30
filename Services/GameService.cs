using LessonNetCore.Models;

namespace LessonNetCore.Services
{
    public class GameService
    {
        // Статический список для хранения игр в памяти
        private static List<Game> _games = new List<Game>
        {
            new Game { Id = 1, Name = "Игра 1", Genre = "Экшен", Author = "AAA", ReleaseDate = new DateTime(2020, 1, 1) },
            new Game { Id = 2, Name = "Игра 2", Genre = "Приключения", Author = "BBB", ReleaseDate = new DateTime(2021, 5, 15) }
        };

        // Получение всех игр с фильтрацией по автору и жанру
        public IEnumerable<Game> GetAll(string author = null, string genre = null)
        {
            var query = _games.AsQueryable();
            if (!string.IsNullOrWhiteSpace(author))
            {
                query = query.Where(g => g.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(g => g.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
            }
            return query.ToList();
        }

        // Получение игры по Id
        public Game GetById(int id)
        {
            return _games.FirstOrDefault(g => g.Id == id);
        }

        // Добавление новой игры в список
        public void Add(Game game)
        {
            // Присваиваем новый Id
            game.Id = _games.Any() ? _games.Max(g => g.Id) + 1 : 1;
            _games.Add(game);
        }

        // Обновление информации об игре
        public void Update(Game game)
        {
            var existing = GetById(game.Id);
            if (existing != null)
            {
                existing.Name = game.Name;
                existing.Genre = game.Genre;
                existing.Author = game.Author;
                existing.ReleaseDate = game.ReleaseDate;
            }
        }
        public void Delete(int id)
        {
            var game = GetById(id);
            if (game != null)
            {
                _games.Remove(game);
            }
        }
    }
}
