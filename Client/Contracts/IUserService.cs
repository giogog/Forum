using Client.Models;

namespace Client.Contracts;

public interface IUserService
{
    Task<ApiResponse<UserResult>> GetUserWithEmail(string email);
    Task<ApiResponse<AuthorizedUserResult>> GetAuthorizedUserData(int id);
    Task<ApiResponse<Result>> UpdateAuthrizedUserData(int id, AuthorizedUserResult authorizedUserResult);
}
