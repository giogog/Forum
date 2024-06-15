
using Client.Contracts;
using Client.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Security.Claims;
using Blazored.LocalStorage;

namespace Client.Services;

public class JwtService:IJwtService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public JwtService(HttpClient httpClient,
                       AuthenticationStateProvider authenticationStateProvider,
                       ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
    }


    public async Task SetAuthToken(string token)
    {
        await _localStorage.SetItemAsync("authToken", token);
    }

    public async Task<string> GetAuthToken()
    {
        return await _localStorage.GetItemAsync<string>("authToken");
    }

    public void SetAuthorizationHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
    }

    public string? GetRoleFromToken(string token)
    {
        var claims = ((ApiAuthenticationStateProvider)_authenticationStateProvider).ParseClaimsFromJwt(token);
        return claims.FirstOrDefault(c => c.Type == "role")?.Value;

    }
    public string GetRoleFromClaim(IEnumerable<Claim> claims) 
    { 
        return claims.FirstOrDefault(c => c.Type == "role")?.Value;
    }

    public string GetUsernameFromClaim(IEnumerable<Claim> claims)
    {
        return claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
    }
}
