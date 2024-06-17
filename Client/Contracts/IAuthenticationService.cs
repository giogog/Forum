using Client.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Contracts
{
    public interface IAuthenticationService
    {
        public void MarkUserAsAuthenticated(string username);
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task Logout();
    }
}