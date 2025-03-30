using LessonNetCore.Models;
namespace LessonNetCore.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
    }
}
