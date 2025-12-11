using DTOs.Auth;
using System.Threading.Tasks;

namespace API.Auth.Blazor
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task<bool> IsAuthenticatedAsync();
        Task<string> GetTokenAsync();
    }
}