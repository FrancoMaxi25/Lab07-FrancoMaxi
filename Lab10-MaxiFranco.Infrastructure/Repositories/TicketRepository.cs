using Lab10_MaxiFranco.Domain.Entities;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;
using Lab10_MaxiFranco.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab10_MaxiFranco.Infrastructure.Repositories;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    public TicketRepository(TicketeraDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId)
    {
        return await DbSet
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByStatusAsync(string status)
    {
        return await DbSet
            .AsNoTracking()
            .Where(t => t.Status == status)
            .ToListAsync();
    }

    public async Task<Ticket?> GetWithResponsesAsync(Guid ticketId)
    {
        return await DbSet
            .AsNoTracking()
            .Include(t => t.Responses)
            .ThenInclude(r => r.Responder)
            .FirstOrDefaultAsync(t => t.TicketId == ticketId);
    }
}