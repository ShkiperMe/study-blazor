using System.Threading.Tasks;
using System.Collections.Generic;
using Fridge.Shared;

namespace Fridge.Client.Services.AuthService
{
    public interface IAuthService
    {
        List<User> Users { get; set; }
        Task GetUsers();
        Task<ServiceResponse<User>> GetUser(int id);
        Task<ServiceResponse<int>> Register(UserRegister request);
        Task<ServiceResponse<string>> Login(UserLogin request);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request);
        Task<bool> IsUserAuthenticated();
    }
}
