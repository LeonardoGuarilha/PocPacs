using Application.Handlers.QueryHandlers.Studies.ListStudies;
using Domain.Repositories.Read;
using Infra.Repositories.Postgres.Read;
using MediatR;

namespace Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddMediator();
        services.AddRepositories();

        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IQidoRSRepository, QidoRSRepository>();

        return services;
    }

    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ListStudies));

        return services;
    }
}
