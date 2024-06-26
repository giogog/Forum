﻿using Client.Models;

namespace Client.Contracts;

public interface IUserService
{
    Task<ApiResponse<UserResult>> GetUserWithEmail(string email);
    Task<ApiResponse<AuthorizedUserResult>> GetAuthorizedUserData(int id);
    Task<ApiResponse<Result>> UpdateAuthrizedUserData(int id, AuthorizedUserResult authorizedUserResult);
    Task<ApiResponse<Result>> BanUser(int id, Ban ban);
    Task<PaginatedApiResponse<IEnumerable<AuthorizedUserResult>>> GetUsers(int page);
    Task<ApiResponse<Result>> ChangeUserModeratorStatus(int userId);
    Task<ApiResponse<IEnumerable<string>>> GetUserRoles(int userId);
}
