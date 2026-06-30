using EAgendaWeb.WebApp.ModuloContato.Aplicacao;
using EAgendaWeb.WebApp.ModuloCompromisso.Aplicacao;
using EAgendaWeb.WebApp.ModuloCategoria.Aplicacao;
using EAgendaWeb.WebApp.ModuloDespesa.Aplicacao;
using EAgendaWeb.WebApp.ModuloTarefa.Aplicacao;

namespace EAgendaWeb.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<ServicoContato>();
        services.AddScoped<ServicoCompromisso>();
        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoDespesa>();
        services.AddScoped<ServicoTarefa>();
        services.AddScoped<ServicoItemTarefa>();

        return services;
    }
}