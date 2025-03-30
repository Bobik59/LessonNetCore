using LessonNetCore.Models;
using LessonNetCore.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace LessonNetCore.Pages.Games
{
    public class DetailsModel : PageModel
    {
        private readonly GameService _gameService;
        public Game Game { get; set; }

        public DetailsModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult OnGet(int id)
        {
            Game = _gameService.GetById(id);
            if (Game == null)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
