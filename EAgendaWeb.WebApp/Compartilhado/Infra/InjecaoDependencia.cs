using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.ModuloContato.Dominio;
using EAgendaWeb.WebApp.ModuloContato.Infra;
using EAgendaWeb.WebApp.ModuloCompromisso.Dominio;
using EAgendaWeb.WebApp.ModuloCompromisso.Infra;
using EAgendaWeb.WebApp.ModuloCategoria.Dominio;
using EAgendaWeb.WebApp.ModuloCategoria.Infra;
using EAgendaWeb.WebApp.ModuloDespesa.Dominio;
using EAgendaWeb.WebApp.ModuloDespesa.Infra;

namespace EAgendaWeb.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static IServiceCollection AddInfraRepositories(
        this IServiceCollection services)
    {
        services.AddSingleton<SqlConnectionFactory>();

        services.AddScoped<IRepositorioContato,
            RepositorioContatoEmSql>();

        services.AddScoped<IRepositorioCompromisso,
            RepositorioCompromissoEmSql>();

        services.AddScoped<IRepositorioCategoria,
            RepositorioCategoriaEmSql>();

        services.AddScoped<IRepositorioDespesa,
            RepositorioDespesaEmSql>();

        return services;
    }
}