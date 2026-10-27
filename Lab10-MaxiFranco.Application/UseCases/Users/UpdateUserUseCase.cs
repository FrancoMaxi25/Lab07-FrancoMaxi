using Lab10_MaxiFranco.Application.DTOs.Users;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;
namespace Lab10_MaxiFranco.Application.UseCases.Users;
public class UpdateUserUseCase(IUnitOfWork uow)
{
    public async Task<bool> ExecuteAsync(Guid id, UpdateUserDto dto)
    {
        var u = await uow.Users.GetByIdAsync(id); if (u is null) return false;
        var username = dto.Username?.Trim() ?? ""; var email = dto.Email?.Trim() ?? "";
        if (username.Length == 0 || email.Length == 0) throw new ArgumentException("Nombre y correo obligatorios.");
        var other = await uow.Users.GetByUsernameAsync(username);
        if (other is not null && other.UserId != id) throw new InvalidOperationException("Usuario duplicado.");
        other = await uow.Users.GetByEmailAsync(email);
        if (other is not null && other.UserId != id) throw new InvalidOperationException("Correo duplicado.");
        u.Username = username; u.Email = email; uow.Users.Update(u); await uow.SaveChangesAsync(); return true;
    }
}
