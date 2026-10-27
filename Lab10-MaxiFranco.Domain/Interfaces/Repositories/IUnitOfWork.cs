namespace Lab10_MaxiFranco.Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    IUserRoleRepository UserRoles { get; }
    ITicketRepository Tickets { get; }
    IResponseRepository Responses { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}