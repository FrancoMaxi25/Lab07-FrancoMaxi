namespace Lab10_MaxiFranco.Application.DTOs.Responses;

public class UpdateResponseDto
{
    public Guid TicketId { get; set; }
    public Guid ResponderId { get; set; }
    public string Message { get; set; } = string.Empty;
}
