namespace GoPractice.Application.Interfaces;

public interface IJwtTokenService
{
    (string AccessToken, DateTime ExpiresAt) CreateToken(string userId, string userName, IEnumerable<string> roles);
}
