using GoPractice.Application.Dtos.Auth;
using GoPractice.Application.Interfaces;
using GoPractice.Shared.Exceptions;
using GoPractice.Shared.Options;
using Microsoft.Extensions.Options;

namespace GoPractice.Infrastructure.Services;

public class DemoAuthService : IAuthService
{
    private readonly AuthDemoOptions _authDemoOptions;
    private readonly JwtOptions _jwtOptions;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ICurrentUser _currentUser;

    public DemoAuthService(
        IOptions<AuthDemoOptions> authDemoOptions,
        IOptions<JwtOptions> jwtOptions,
        IJwtTokenService jwtTokenService,
        ICurrentUser currentUser)
    {
        _authDemoOptions = authDemoOptions.Value;
        _jwtOptions = jwtOptions.Value;
        _jwtTokenService = jwtTokenService;
        _currentUser = currentUser;
    }

    public Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        if (!_authDemoOptions.Enabled)
        {
            throw new BusinessException("演示登录已关闭，请替换为真实认证服务。");
        }

        if (!_jwtOptions.Enabled)
        {
            throw new BusinessException("JWT 未启用，请先在配置中打开 Jwt:Enabled。");
        }

        if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new BusinessException("账号和密码不能为空。");
        }

        var user = _authDemoOptions.Users.FirstOrDefault(x =>
            string.Equals(x.UserName, request.UserName.Trim(), StringComparison.OrdinalIgnoreCase));

        if (user is null || !string.Equals(user.Password, request.Password, StringComparison.Ordinal))
        {
            throw new BusinessException("账号或密码错误。", 4010);
        }

        var token = _jwtTokenService.CreateToken(user.UserId, user.UserName, user.Roles);
        return Task.FromResult(new LoginResponse
        {
            AccessToken = token.AccessToken,
            ExpiresAt = token.ExpiresAt,
            UserId = user.UserId,
            UserName = user.UserName,
            Roles = user.Roles
        });
    }

    public Task<CurrentUserDto> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new CurrentUserDto
        {
            UserId = _currentUser.UserId,
            UserName = _currentUser.UserName,
            IsAuthenticated = _currentUser.IsAuthenticated,
            Roles = _currentUser.Roles.ToArray()
        });
    }
}
