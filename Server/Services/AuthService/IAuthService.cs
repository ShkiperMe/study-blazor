using System.Threading.Tasks;
using System.Collections.Generic;
using Fridge.Shared;

namespace Fridge.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<List<User>>> GetUsersAsync();
        Task<ServiceResponse<User>> GetUserAsync(int userId);
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
        int GetUserId();
        string GetUserEmail();
        Task<User> GetUserByEmail(string email);
    }
}
