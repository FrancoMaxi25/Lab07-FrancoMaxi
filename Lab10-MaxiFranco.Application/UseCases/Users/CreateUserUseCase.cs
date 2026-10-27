using Lab10_MaxiFranco.Application.DTOs.Users;
using Lab10_MaxiFranco.Application.Interfaces.Services;
using Lab10_MaxiFranco.Domain.Entities;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;

namespace Lab10_MaxiFranco.Application.UseCases.Users;

public class CreateUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserUseCase(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> ExecuteAsync(CreateUserDto dto)
    {
        var username = dto.Username.Trim();
        var email = dto.Email.Trim();

        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException(
                "El nombre de usuario es obligatorio.");

        var existingUser =
            await _unitOfWork.Users.GetByUsernameAsync(username);

        if (existingUser != null)
            throw new InvalidOperationException(
                "El nombre de usuario ya existe.");

        var existingEmail =
            await _unitOfWork.Users.GetByEmailAsync(email);

        if (existingEmail != null)
            throw new InvalidOperationException(
                "El correo electrónico ya está registrado.");

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = username,
            Email = email,
            PasswordHash = _passwordHasher.Hash(dto.Password),
            CreatedAt = DateTime.SpecifyKind(
                DateTime.UtcNow,
                DateTimeKind.Unspecified)
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return new UserDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }
}