using Client.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Contracts
{
    public interface IAuthService
    {
        Task<ApiResponse<RegisterResult>> Register(RegisterModelDto registerModel);
        Task<ApiResponse<LoginResult>> Login(LoginModel loginModel);
        public void MarkUserAsAuthenticated(string username);
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task<ApiResponse<RegisterResult>> ResendConfiramtionsMail(string username);
        Task<ApiResponse<Result>> RequestPasswordReset(string email);
        Task<ApiResponse<Result>> PasswordReset(ResetPasswordDto resetPasswordDto);
        Task Logout();
    }
}