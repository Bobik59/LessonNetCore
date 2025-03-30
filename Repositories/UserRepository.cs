using LessonNetCore.Models;
namespace LessonNetCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public Task<User?> GetUserByUsernameAsync(string username)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Username == username));
        }

        public Task AddUserAsync(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }
    }
}
