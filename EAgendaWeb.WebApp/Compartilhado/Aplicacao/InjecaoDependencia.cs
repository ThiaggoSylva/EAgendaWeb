using EAgendaWeb.WebApp.ModuloContato.Aplicacao;
using EAgendaWeb.WebApp.ModuloCompromisso.Aplicacao;

namespace EAgendaWeb.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<ServicoContato>();
        services.AddScoped<ServicoCompromisso>();

        return services;
    }
}