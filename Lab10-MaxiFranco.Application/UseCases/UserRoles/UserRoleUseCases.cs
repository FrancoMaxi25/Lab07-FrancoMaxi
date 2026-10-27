using Lab10_MaxiFranco.Application.DTOs.UserRoles;
using Lab10_MaxiFranco.Domain.Entities;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;

namespace Lab10_MaxiFranco.Application.UseCases.UserRoles;
public class UserRoleUseCases(IUnitOfWork uow)
{
    private static UserRoleDto Map(UserRole ur) => new() { UserId = ur.UserId, RoleId = ur.RoleId, AssignedAt = ur.AssignedAt };
    public async Task<List<UserRoleDto>> GetAllAsync() => (await uow.UserRoles.GetAllAsync()).Select(Map).ToList();
    public async Task<List<UserRoleDto>> GetByUserAsync(Guid userId) => (await uow.UserRoles.GetByUserIdAsync(userId)).Select(Map).ToList();
    public async Task<UserRoleDto> AssignAsync(AssignRoleDto dto)
    {
        if (await uow.Users.GetByIdAsync(dto.UserId) is null) throw new ArgumentException("Usuario inexistente.");
        if (await uow.Roles.GetByIdAsync(dto.RoleId) is null) throw new ArgumentException("Rol inexistente.");
        if (await uow.UserRoles.GetByIdAsync(dto.UserId, dto.RoleId) is not null) throw new InvalidOperationException("Rol ya asignado.");
        var ur = new UserRole { UserId = dto.UserId, RoleId = dto.RoleId,
            AssignedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified) };
        await uow.UserRoles.AddAsync(ur); await uow.SaveChangesAsync(); return Map(ur);
    }
    public async Task<bool> RemoveAsync(Guid userId, Guid roleId)
    {
        var ur = await uow.UserRoles.GetByIdAsync(userId, roleId); if (ur is null) return false;
        uow.UserRoles.Delete(ur); await uow.SaveChangesAsync(); return true;
    }
}
