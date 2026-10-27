using System.ComponentModel.DataAnnotations;

namespace Lab10_MaxiFranco.Application.DTOs.Users;

public class UpdateUserDto
{
    [Required]
    [StringLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;
}