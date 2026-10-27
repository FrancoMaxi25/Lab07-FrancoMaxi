using Lab10_MaxiFranco.Application.DTOs.Users;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;
namespace Lab10_MaxiFranco.Application.UseCases.Users;
public class GetUsersUseCase(IUnitOfWork uow)
{
    public async Task<List<UserDto>> ExecuteAsync() => (await uow.Users.GetAllAsync())
        .Select(u => new UserDto { UserId = u.UserId, Username = u.Username, Email = u.Email, CreatedAt = u.CreatedAt }).ToList();
}
