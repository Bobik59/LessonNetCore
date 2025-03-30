using LessonNetCore.Models;
using LessonNetCore.Repositories;
namespace LessonNetCore.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;

        public AuthService(IUserRepository userRepository, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
        }

        // Метод регистрации (существует уже)
        public async Task<bool> RegisterAsync(string username, string password)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                return false; // Пользователь уже существует
            }

            var hashedPassword = _encryptionService.HashPassword(password);
            var newUser = new User
            {
                Username = username,
                HashedPassword = hashedPassword
            };

            await _userRepository.AddUserAsync(newUser);
            return true;
        }

        // Метод аутентификации
        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return false; // Пользователь не найден
            }

            // Сравнение введенного пароля с хешированным
            return _encryptionService.VerifyPassword(password, user.HashedPassword);
        }
    }

}
