﻿using Client.Contracts;
using Client.Models;

namespace Client.Services;

public class AccountService:IAccountService
{
    private readonly IHttpRequestService<Result> _request;
    public AccountService(IHttpRequestService<Result> request)
    {
        _request = request;
    }
    public async Task<ApiResponse<RegisterResult>> Register(RegisterModelDto registerModel)
    {

        return await _request.RequestAsync<RegisterResult>(new ApiRequest(ApiType.POST, "Account/register", registerModel));
    }

    public async Task<ApiResponse<LoginResult>> Login(LoginModel loginModel)
    {
        return await _request.RequestAsync<LoginResult>(new ApiRequest(ApiType.POST, "Account/login", loginModel));
    }

    public async Task<ApiResponse<RegisterResult>> ResendConfiramtionsMail(string username)
    {
        return await _request.RequestAsync<RegisterResult>(new ApiRequest(ApiType.POST, $"Account/resend-confirmation/{username}", null));
    }

    public async Task<ApiResponse<Result>> RequestPasswordReset(string email)
    {
        return await _request.RequestAsync<Result>(new ApiRequest(ApiType.POST, $"Account/request-password-reset/{email}", null));
    }
    public async Task<ApiResponse<Result>> PasswordReset(ResetPasswordDto resetPasswordDto)
    {
        return await _request.RequestAsync<Result>(new ApiRequest(ApiType.PUT, $"Account/reset-password", resetPasswordDto));
    }

}
