using Lab10_MaxiFranco.Application.DTOs.Auth;
using Lab10_MaxiFranco.Application.Interfaces.Services;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;
namespace Lab10_MaxiFranco.Application.UseCases.Auth;
public class LoginUseCase(IUnitOfWork uow, IPasswordHasher hasher, ITokenService tokens)
{
    public async Task<LoginResponseDto?> ExecuteAsync(LoginDto dto)
    {
        var user = await uow.Users.GetByUsernameAsync(dto.Username);
        if (user is null || !hasher.Verify(dto.Password, user.PasswordHash)) return null;
        var assignments = await uow.UserRoles.GetByUserIdAsync(user.UserId);
        var names = new List<string>();
        foreach (var assignment in assignments)
        {
            var role = await uow.Roles.GetByIdAsync(assignment.RoleId);
            if (role is not null) names.Add(role.RoleName);
        }
        var (token, expiresAt) = tokens.Generate(user, names);
        return new LoginResponseDto { Token = token, ExpiresAt = expiresAt, Username = user.Username };
    }
}
