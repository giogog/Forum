using Domain.Entities;
using Domain.Models;
using System.Security.Claims;

namespace Contracts;

public interface IUserService
{
    Task<User> GetUserWithClaim(ClaimsPrincipal principal);
    Task<UserDto> GetUserWithEmail(string email);
    Task<AuthorizedUserDto> GetAuthorizedUserData(int id);
    Task UpdateAuthorizedUser(int id, AuthorizedUserDto authorizedUserDto);
    Task<IEnumerable<UserDto>> GetUsers();
    Task UserBanStatusChange(int userId, Ban ban);
} 
