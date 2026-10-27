using Lab10_MaxiFranco.Domain.Entities;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;
using Lab10_MaxiFranco.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab10_MaxiFranco.Infrastructure.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(TicketeraDbContext context) : base(context)
    {
    }

    public async Task<Role?> GetByNameAsync(string roleName)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RoleName == roleName);
    }
}