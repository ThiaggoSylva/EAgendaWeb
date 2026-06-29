using EAgendaWeb.WebApp.ModuloContato.Aplicacao;
using EAgendaWeb.WebApp.ModuloCompromisso.Aplicacao;
using EAgendaWeb.WebApp.ModuloCategoria.Aplicacao;

namespace EAgendaWeb.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<ServicoContato>();
        services.AddScoped<ServicoCompromisso>();
        services.AddScoped<ServicoCategoria>();

        return services;
    }
}