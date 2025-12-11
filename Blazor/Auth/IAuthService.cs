using DTOs.Auth;
using System.Threading.Tasks;

namespace Blazor.Auth
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task<bool> IsAuthenticatedAsync();
        Task<string> GetTokenAsync();
    }
}