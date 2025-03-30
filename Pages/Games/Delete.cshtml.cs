using LessonNetCore.Models;
using LessonNetCore.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace LessonNetCore.Pages.Games
{
    public class DeleteModel : PageModel
    {
        private readonly GameService _gameService;
        public Game Game { get; set; }

        public DeleteModel(GameService gameService)
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

        public IActionResult OnPost(int id)
        {
            _gameService.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}
