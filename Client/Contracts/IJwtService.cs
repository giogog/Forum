using System.Security.Claims;

namespace Client.Contracts;

public interface IJwtService
{
    public void SetAuthorizationHeader(string token);
    string? GetRoleFromToken(string token);
    Task SetAuthToken(string token);
    Task<string> GetAuthToken();
    string GetRoleFromClaim(IEnumerable<Claim> claims);
    string GetUsernameFromClaim(IEnumerable<Claim> claims);
}
