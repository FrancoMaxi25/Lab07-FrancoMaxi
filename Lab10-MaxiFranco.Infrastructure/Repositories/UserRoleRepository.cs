using Lab10_MaxiFranco.Domain.Entities;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;
using Lab10_MaxiFranco.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab10_MaxiFranco.Infrastructure.Repositories;

public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(TicketeraDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserRole>> GetByUserIdAsync(Guid userId)
    {
        return await DbSet
            .AsNoTracking()
            .Where(ur => ur.UserId == userId)
            .ToListAsync();
    }
}