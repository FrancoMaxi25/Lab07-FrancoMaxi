using Lab10_MaxiFranco.Application.UseCases.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Lab10_MaxiFranco.Api.Configuration;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        var useCaseTypes = typeof(LoginUseCase).Assembly
            .GetTypes()
            .Where(type =>
                type.IsClass &&
                !type.IsAbstract &&
                type.IsPublic &&
                type.Name.EndsWith("UseCase"));

        foreach (var useCaseType in useCaseTypes)
        {
            services.AddScoped(useCaseType);
        }

        return services;
    }
}