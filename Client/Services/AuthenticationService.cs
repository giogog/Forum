using Blazored.LocalStorage;
using Client.Contracts;
using Client.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;


        public AuthenticationService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }



        public void MarkUserAsAuthenticated(string username)
        {
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(username);
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await ((ApiAuthenticationStateProvider)_authenticationStateProvider).GetAuthenticationStateAsync();
        }
    }
}
