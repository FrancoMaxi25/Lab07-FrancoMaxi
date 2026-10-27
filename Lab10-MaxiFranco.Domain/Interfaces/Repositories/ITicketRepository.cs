using Lab10_MaxiFranco.Domain.Entities;

namespace Lab10_MaxiFranco.Domain.Interfaces.Repositories;

public interface ITicketRepository : IGenericRepository<Ticket>
{
    Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Ticket>> GetByStatusAsync(string status);
    Task<Ticket?> GetWithResponsesAsync(Guid ticketId);
}