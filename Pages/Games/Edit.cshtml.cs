using LessonNetCore.Models;
using LessonNetCore.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace LessonNetCore.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly GameService _gameService;
        [BindProperty]
        public Game Game { get; set; }

        public EditModel(GameService gameService)
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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _gameService.Update(Game);
            return RedirectToPage("./Index");
        }
    }
}
