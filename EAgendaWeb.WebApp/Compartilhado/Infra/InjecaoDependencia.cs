using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.ModuloContato.Dominio;
using EAgendaWeb.WebApp.ModuloContato.Infra;

namespace EAgendaWeb.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static IServiceCollection AddInfraRepositories(
        this IServiceCollection services)
    {
        services.AddSingleton<SqlConnectionFactory>();

        services.AddScoped<IRepositorioContato,
            RepositorioContatoEmSql>();

        return services;
    }
}