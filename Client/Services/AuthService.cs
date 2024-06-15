using Blazored.LocalStorage;
using Client.Contracts;
using Client.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpRequestService<Result> _request;
        private readonly string _apiUrl;


        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage,
                           IHttpRequestService<Result> request)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _apiUrl = "https://localhost:5001/";
            _request = request;
        }

        public async Task<ApiResponse<RegisterResult>> Register(RegisterModelDto registerModel)
        {

            return await _request.SendAsync<RegisterResult>(new ApiRequest(ApiType.POST, "Account/register", registerModel));
        }

        public async Task<ApiResponse<LoginResult>> Login(LoginModel loginModel)
        {
            return await _request.SendAsync<LoginResult>(new ApiRequest(ApiType.POST, "Account/login", loginModel));
        }

        public async Task<ApiResponse<RegisterResult>> ResendConfiramtionsMail(string username)
        {
            return await _request.SendAsync<RegisterResult>(new ApiRequest(ApiType.POST, $"Account/resend-confirmation/{username}", null));
        }

        public async Task<ApiResponse<Result>> RequestPasswordReset(string email)
        {
            return await _request.SendAsync<Result>(new ApiRequest(ApiType.POST, $"Account/request-password-reset/{email}", null));
        }
        public async Task<ApiResponse<Result>> PasswordReset(ResetPasswordDto resetPasswordDto)
        {
            return await _request.SendAsync<Result>(new ApiRequest(ApiType.PUT, $"Account/reset-password", resetPasswordDto));
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

//public async Task<ApiResponse<LoginResult>> Login(LoginModel loginModel)
//{
//    var loginAsJson = JsonSerializer.Serialize(loginModel);
//    var response = await _httpClient.PostAsync($"{_apiUrl}api/Account/login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));

//    return JsonSerializer.Deserialize<ApiResponse<LoginResult>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

//}
//public async Task<ApiResponse> Register(RegisterModelDto registerModel)
//{
//  var result = await _httpClient.PostAsync($"{_apiUrl}api/Account/resend-confirmation/{username}", null);
//  return JsonSerializer.Deserialize<ApiResponse<RegisterResult>>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

//    var registerAsJson = JsonSerializer.Serialize(registerModel);
//    var result = await _httpClient.PostAsync($"{_apiUrl}api/Account/register", new StringContent(registerAsJson, Encoding.UTF8, "application/json"));

//    return JsonSerializer.Deserialize<ApiResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
//}