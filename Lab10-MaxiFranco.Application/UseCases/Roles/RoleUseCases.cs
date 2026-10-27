using Lab10_MaxiFranco.Application.DTOs.Roles;
using Lab10_MaxiFranco.Domain.Entities;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;

namespace Lab10_MaxiFranco.Application.UseCases.Roles;

public class RoleUseCases(IUnitOfWork uow)
{
    private static RoleDto Map(Role r) => new() { RoleId = r.RoleId, RoleName = r.RoleName };
    public async Task<List<RoleDto>> GetAllAsync() => (await uow.Roles.GetAllAsync()).Select(Map).ToList();
    public async Task<RoleDto?> GetByIdAsync(Guid id) { var r = await uow.Roles.GetByIdAsync(id); return r is null ? null : Map(r); }
    public async Task<RoleDto> CreateAsync(CreateRoleDto dto)
    {
        var name = dto.RoleName?.Trim() ?? "";
        if (name.Length == 0 || name.Length > 50) throw new ArgumentException("Nombre de rol inválido.");
        if (await uow.Roles.GetByNameAsync(name) is not null) throw new InvalidOperationException("El rol ya existe.");
        var role = new Role { RoleId = Guid.NewGuid(), RoleName = name };
        await uow.Roles.AddAsync(role); await uow.SaveChangesAsync(); return Map(role);
    }
    public async Task<bool> UpdateAsync(Guid id, UpdateRoleDto dto)
    {
        var role = await uow.Roles.GetByIdAsync(id); if (role is null) return false;
        var name = dto.RoleName?.Trim() ?? "";
        if (name.Length == 0 || name.Length > 50) throw new ArgumentException("Nombre de rol inválido.");
        var other = await uow.Roles.GetByNameAsync(name);
        if (other is not null && other.RoleId != id) throw new InvalidOperationException("El rol ya existe.");
        role.RoleName = name; uow.Roles.Update(role); await uow.SaveChangesAsync(); return true;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var role = await uow.Roles.GetByIdAsync(id); if (role is null) return false;
        uow.Roles.Delete(role); await uow.SaveChangesAsync(); return true;
    }
}
