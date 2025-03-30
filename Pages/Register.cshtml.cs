using LessonNetCore.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public class RegisterModel : PageModel
{
    private readonly AuthService _authService;

    public RegisterModel(AuthService authService)
    {
        _authService = authService;
    }

    [BindProperty]
    public string Username { get; set; }
    [BindProperty]
    public string Password { get; set; }
    public string? Message { get; set; }

    public async Task OnPostAsync()
    {
        if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
        {
            var success = await _authService.RegisterAsync(Username, Password);
            if (success)
            {
                Message = "Регистрация успешна!";
            }
            else
            {
                Message = "Пользователь с таким именем уже существует.";
            }
        }
    }
}
