using Lab10_MaxiFranco.Domain.Interfaces.Repositories;
using Lab10_MaxiFranco.Infrastructure.Persistence.Context;

namespace Lab10_MaxiFranco.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TicketeraDbContext _context;

    public IUserRepository Users { get; }
    public IRoleRepository Roles { get; }
    public IUserRoleRepository UserRoles { get; }
    public ITicketRepository Tickets { get; }
    public IResponseRepository Responses { get; }

    public UnitOfWork(
        TicketeraDbContext context,
        IUserRepository users,
        IRoleRepository roles,
        IUserRoleRepository userRoles,
        ITicketRepository tickets,
        IResponseRepository responses)
    {
        _context = context;
        Users = users;
        Roles = roles;
        UserRoles = userRoles;
        Tickets = tickets;
        Responses = responses;
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}