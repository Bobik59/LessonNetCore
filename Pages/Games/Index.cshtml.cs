using LessonNetCore.Models;
using LessonNetCore.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace LessonNetCore.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly GameService _gameService;
        public IEnumerable<Game> Games { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Author { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Genre { get; set; }

        public IndexModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public void OnGet()
        {
            Games = _gameService.GetAll(Author, Genre);
        }
    }
}
