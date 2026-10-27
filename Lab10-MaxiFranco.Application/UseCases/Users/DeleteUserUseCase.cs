using Lab10_MaxiFranco.Domain.Interfaces.Repositories;
namespace Lab10_MaxiFranco.Application.UseCases.Users;
public class DeleteUserUseCase(IUnitOfWork uow)
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        var u = await uow.Users.GetByIdAsync(id); if (u is null) return false;
        uow.Users.Delete(u); await uow.SaveChangesAsync(); return true;
    }
}
