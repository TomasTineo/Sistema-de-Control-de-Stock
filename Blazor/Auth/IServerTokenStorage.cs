using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Auth
{
    public interface IServerTokenStorage
    {
        Task SaveTokenAsync(string token);
        Task<string> GetTokenAsync();
        Task RemoveTokenAsync();
    }
}