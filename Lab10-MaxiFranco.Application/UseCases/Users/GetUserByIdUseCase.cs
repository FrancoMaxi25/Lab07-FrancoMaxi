using Lab10_MaxiFranco.Application.DTOs.Users;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;
namespace Lab10_MaxiFranco.Application.UseCases.Users;
public class GetUserByIdUseCase(IUnitOfWork uow)
{
    public async Task<UserDto?> ExecuteAsync(Guid id)
    {
        var u = await uow.Users.GetByIdAsync(id);
        return u is null ? null : new UserDto { UserId = u.UserId, Username = u.Username, Email = u.Email, CreatedAt = u.CreatedAt };
    }
}
