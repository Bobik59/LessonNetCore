using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LessonNetCore.Services;
using System.Threading.Tasks;
using LessonNetCore.Services;

public class LoginModel : PageModel
{
    private readonly AuthService _authService;

    public LoginModel(AuthService authService)
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
            var isAuthenticated = await _authService.AuthenticateAsync(Username, Password);
            if (isAuthenticated)
            {
                Message = "Добро пожаловать, " + Username + "!";
            }
            else
            {
                Message = "Неверное имя пользователя или пароль.";
            }
        }
    }
}
