namespace Lab10_MaxiFranco.Application.DTOs.Tickets;

public class CreateTicketDto
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = string.Empty;
}
