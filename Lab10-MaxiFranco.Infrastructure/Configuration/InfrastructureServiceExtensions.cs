
using Lab10_MaxiFranco.Application.Interfaces.Services;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;
using Lab10_MaxiFranco.Infrastructure.Persistence.Context;
using Lab10_MaxiFranco.Infrastructure.Repositories;
using Lab10_MaxiFranco.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab10_MaxiFranco.Infrastructure.Configuration;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Configuración de PostgreSQL
        var connectionString =
            configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "No se encontró la conexión DefaultConnection.");
        }

        // Registro del DbContext
        services.AddDbContext<TicketeraDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Registro del repositorio genérico
        services.AddScoped(
            typeof(IGenericRepository<>),
            typeof(GenericRepository<>));

        // Registro de repositorios específicos
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IResponseRepository, ResponseRepository>();

        // Registro de Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Registro de seguridad de contraseñas
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        // Registro del servicio JWT
        services.AddScoped<ITokenService, JwtTokenService>();

        return services;
    }
}
