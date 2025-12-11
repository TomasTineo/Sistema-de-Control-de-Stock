using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Blazor.Auth
{
    public class ServerTokenStorage : IServerTokenStorage
    {
        private readonly ProtectedSessionStorage _sessionStorage;

        public ServerTokenStorage(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task SaveTokenAsync(string token)
        {
            await _sessionStorage.SetAsync("authToken", token);
        }

        public async Task<string> GetTokenAsync()
        {
            var result = await _sessionStorage.GetAsync<string>("authToken");
            return result.Success ? result.Value : string.Empty;
        }

        public async Task RemoveTokenAsync()
        {
            await _sessionStorage.DeleteAsync("authToken");
        }
    }
}