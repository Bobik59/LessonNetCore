using LessonNetCore.Models;
using LessonNetCore.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace LessonNetCore.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly GameService _gameService;

        // Свойство для привязки данных новой игры
        [BindProperty]
        public Game NewGame { get; set; }

        public CreateModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public void OnGet()
        {
            NewGame = new Game();
        }

        // POST: Обработка отправки формы создания игры
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Добавление новой игры в список
            _gameService.Add(NewGame);
            return RedirectToPage("./Index");
        }
    }
}
