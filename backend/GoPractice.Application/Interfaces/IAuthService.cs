using GoPractice.Application.Dtos.Auth;

namespace GoPractice.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

    Task<CurrentUserDto> GetCurrentUserAsync(CancellationToken cancellationToken = default);
}
