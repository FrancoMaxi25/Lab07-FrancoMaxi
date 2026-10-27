namespace Lab10_MaxiFranco.Application.DTOs.Tickets;

public class UpdateTicketDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = string.Empty;
}
