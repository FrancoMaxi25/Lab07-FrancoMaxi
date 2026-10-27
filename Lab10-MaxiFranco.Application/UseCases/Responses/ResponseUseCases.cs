using Lab10_MaxiFranco.Application.DTOs.Responses;
using Lab10_MaxiFranco.Domain.Entities;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;

namespace Lab10_MaxiFranco.Application.UseCases.Responses;

public class ResponseUseCases(IUnitOfWork uow)
{
    private static ResponseDto Map(Response r) => new() { ResponseId = r.ResponseId, TicketId = r.TicketId,
        ResponderId = r.ResponderId, Message = r.Message, CreatedAt = r.CreatedAt };
    public async Task<List<ResponseDto>> GetAllAsync() => (await uow.Responses.GetAllAsync()).Select(Map).ToList();
    public async Task<ResponseDto?> GetByIdAsync(Guid id) { var r = await uow.Responses.GetByIdAsync(id); return r is null ? null : Map(r); }
    public async Task<ResponseDto> CreateAsync(CreateResponseDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Message)) throw new ArgumentException("El mensaje es obligatorio.");
        if (await uow.Tickets.GetByIdAsync(dto.TicketId) is null) throw new ArgumentException("Ticket inexistente.");
        if (await uow.Users.GetByIdAsync(dto.ResponderId) is null) throw new ArgumentException("Usuario inexistente.");
        var r = new Response { ResponseId = Guid.NewGuid(), TicketId = dto.TicketId, ResponderId = dto.ResponderId,
            Message = dto.Message.Trim(), CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified) };
        await uow.Responses.AddAsync(r); await uow.SaveChangesAsync(); return Map(r);
    }
    public async Task<bool> UpdateAsync(Guid id, UpdateResponseDto dto)
    {
        var r = await uow.Responses.GetByIdAsync(id); if (r is null) return false;
        if (string.IsNullOrWhiteSpace(dto.Message)) throw new ArgumentException("El mensaje es obligatorio.");
        if (await uow.Tickets.GetByIdAsync(dto.TicketId) is null) throw new ArgumentException("Ticket inexistente.");
        if (await uow.Users.GetByIdAsync(dto.ResponderId) is null) throw new ArgumentException("Usuario inexistente.");
        r.TicketId = dto.TicketId; r.ResponderId = dto.ResponderId; r.Message = dto.Message.Trim();
        uow.Responses.Update(r); await uow.SaveChangesAsync(); return true;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var r = await uow.Responses.GetByIdAsync(id); if (r is null) return false;
        uow.Responses.Delete(r); await uow.SaveChangesAsync(); return true;
    }
}
