using Lab10_MaxiFranco.Domain.Entities;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;
using Lab10_MaxiFranco.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab10_MaxiFranco.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(TicketeraDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}