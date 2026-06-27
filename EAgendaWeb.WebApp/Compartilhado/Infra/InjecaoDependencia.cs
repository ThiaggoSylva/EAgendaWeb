using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;

namespace EAgendaWeb.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static IServiceCollection AddInfraRepositories(
        this IServiceCollection services)
    {
        services.AddSingleton<SqlConnectionFactory>();

        return services;
    }
}