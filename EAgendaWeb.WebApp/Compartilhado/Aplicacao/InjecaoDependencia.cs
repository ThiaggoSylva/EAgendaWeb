using EAgendaWeb.WebApp.ModuloContato.Aplicacao;

namespace EAgendaWeb.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<ServicoContato>();

        return services;
    }
}