using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace LessonNetCore
{
    public interface IDbService
    {
        Task<User> GetUserAsync(string login);
        Task AddUserAsync(User user);
    }

    public class FakeDbService : IDbService
    {
        private readonly List<User> _users = new();

        public Task<User> GetUserAsync(string login)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Login == login));
        }

        public Task AddUserAsync(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }
    }

    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }

    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }

    public interface IPasswordVerifier
    {
        bool VerifyPassword(string enteredPassword, string storedHash);
    }

    public class PasswordVerifier : IPasswordVerifier
    {
        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }
            return true;
        }
    }



    public class User
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }


    public interface IAuthService
    {
        Task<bool> ValidateCredentialsAsync(string login, string password);
        Task RegisterUserAsync(string login, string password);
    }

    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthService _authService;

        public AuthMiddleware(RequestDelegate next, AuthService authService)
        {
            _next = next;
            _authService = authService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/auth"))
            {
                var login = context.Request.Query["login"];
                var password = context.Request.Query["password"];

                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Введи логин, пароль");
                    return;
                }

                if (context.Request.Path.Value.EndsWith("/register"))
                {
                    try
                    {
                        context.Response.ContentType = "text/html; charset=utf-8";
                        await _authService.RegisterAsync(login, password);
                        await context.Response.WriteAsync("Ура регистрация успешна");
                    }
                    catch (InvalidOperationException ex)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync(ex.Message);
                    }
                    return;
                }

                if (context.Request.Path.Value.EndsWith("/login"))
                {
                    if (await _authService.ValidateAsync(login, password))
                    {
                        context.Response.ContentType = "text/html; charset=utf-8";
                        await context.Response.WriteAsync("Ура автроризация успешна");
                    }
                    else
                    {
                        context.Response.ContentType = "text/html; charset=utf-8";
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("проблем");
                    }
                    return;
                }
            }

            await _next(context);
        }
    }
    public class AuthService
    {
        private readonly List<User> _users = new();
        private readonly IPasswordHasher _hasher;
        private readonly IPasswordVerifier _verifier;

        public AuthService(IPasswordHasher hasher, IPasswordVerifier verifier)
        {
            _hasher = hasher;
            _verifier = verifier;
        }

        public Task RegisterAsync(string login, string password)
        {
            if (_users.Any(u => u.Login == login))
                throw new InvalidOperationException("Такой уже есть");

            _users.Add(new User
            {
                Login = login,
                PasswordHash = _hasher.HashPassword(password)
            });

            return Task.CompletedTask;
        }

        public Task<bool> ValidateAsync(string login, string password)
        {
            var user = _users.FirstOrDefault(u => u.Login == login);
            return Task.FromResult(user != null &&
                _verifier.VerifyPassword(password, user.PasswordHash));
        }
    }
}
