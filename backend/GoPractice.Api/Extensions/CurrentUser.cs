using System.Security.Claims;
using GoPractice.Application.Interfaces;

namespace GoPractice.Api.Extensions;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId =>
        FindClaim(ClaimTypes.NameIdentifier)
        ?? FindClaim("sub")
        ?? string.Empty;

    public string UserName =>
        FindClaim(ClaimTypes.Name)
        ?? FindClaim("name")
        ?? "anonymous";

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public IReadOnlyCollection<string> Roles =>
        _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role).Select(x => x.Value).ToArray()
        ?? Array.Empty<string>();

    private string? FindClaim(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(claimType);
    }
}
