namespace EAgendaWeb.WebApp.Compartilhado.Apresentacao;

public static class InjecaoDependencia
{
    public static IServiceCollection AddPresentationConfig(
        this IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddAutoMapper(typeof(Program));

        return services;
    }
}