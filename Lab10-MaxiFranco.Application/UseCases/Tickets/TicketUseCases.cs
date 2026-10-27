using Lab10_MaxiFranco.Application.DTOs.Tickets;
using Lab10_MaxiFranco.Domain.Entities;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;

namespace Lab10_MaxiFranco.Application.UseCases.Tickets;

public class TicketUseCases(IUnitOfWork uow)
{
    private static TicketDto Map(Ticket t) => new() { TicketId = t.TicketId, UserId = t.UserId, Title = t.Title,
        Description = t.Description, Status = t.Status, CreatedAt = t.CreatedAt, ClosedAt = t.ClosedAt };
    private static void Validate(string title, string status)
    {
        if (string.IsNullOrWhiteSpace(title) || title.Length > 255) throw new ArgumentException("Título inválido.");
        if (status is not ("abierto" or "en_proceso" or "cerrado")) throw new ArgumentException("Estado inválido.");
    }
    public async Task<List<TicketDto>> GetAllAsync() => (await uow.Tickets.GetAllAsync()).Select(Map).ToList();
    public async Task<TicketDto?> GetByIdAsync(Guid id) { var t = await uow.Tickets.GetByIdAsync(id); return t is null ? null : Map(t); }
    public async Task<TicketDto> CreateAsync(CreateTicketDto dto)
    {
        Validate(dto.Title, dto.Status);
        if (await uow.Users.GetByIdAsync(dto.UserId) is null) throw new ArgumentException("El usuario no existe.");
        var t = new Ticket { TicketId = Guid.NewGuid(), UserId = dto.UserId, Title = dto.Title.Trim(),
            Description = dto.Description, Status = dto.Status, CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
            ClosedAt = dto.Status == "cerrado" ? DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified) : null };
        await uow.Tickets.AddAsync(t); await uow.SaveChangesAsync(); return Map(t);
    }
    public async Task<bool> UpdateAsync(Guid id, UpdateTicketDto dto)
    {
        var t = await uow.Tickets.GetByIdAsync(id); if (t is null) return false;
        Validate(dto.Title, dto.Status);
        t.Title = dto.Title.Trim(); t.Description = dto.Description; t.Status = dto.Status;
        t.ClosedAt = dto.Status == "cerrado" ? t.ClosedAt ?? DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified) : null;
        uow.Tickets.Update(t); await uow.SaveChangesAsync(); return true;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var t = await uow.Tickets.GetByIdAsync(id); if (t is null) return false;
        uow.Tickets.Delete(t); await uow.SaveChangesAsync(); return true;
    }
}
