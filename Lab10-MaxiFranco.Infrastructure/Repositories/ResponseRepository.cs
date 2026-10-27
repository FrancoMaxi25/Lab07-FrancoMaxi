using Lab10_MaxiFranco.Domain.Entities;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;
using Lab10_MaxiFranco.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab10_MaxiFranco.Infrastructure.Repositories;

public class ResponseRepository : GenericRepository<Response>, IResponseRepository
{
    public ResponseRepository(TicketeraDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Response>> GetByTicketIdAsync(Guid ticketId)
    {
        return await DbSet
            .AsNoTracking()
            .Where(r => r.TicketId == ticketId)
            .ToListAsync();
    }
}