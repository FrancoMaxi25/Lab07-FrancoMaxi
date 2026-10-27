using System.ComponentModel.DataAnnotations;

namespace Lab10_MaxiFranco.Application.DTOs.Users;

public class CreateUserDto
{
    [Required]
    [StringLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}