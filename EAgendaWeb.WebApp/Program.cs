using EAgendaWeb.WebApp.Compartilhado.Aplicacao;
using EAgendaWeb.WebApp.Compartilhado.Apresentacao;
using EAgendaWeb.WebApp.Compartilhado.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraRepositories();

builder.Services.AddApplicationServices();

builder.Services.AddPresentationConfig();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();