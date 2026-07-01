using EAgendaWeb.WebApp.Compartilhado.Aplicacao;
using EAgendaWeb.WebApp.Compartilhado.Apresentacao;
using EAgendaWeb.WebApp.Compartilhado.Infra;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddInfraRepositories();

builder.Services.AddApplicationServices();

builder.Services.AddPresentationConfig();

var licenseKey =
    builder.Configuration["NewRelic:LicenseKey"];

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception =
            context.Features
                .Get<IExceptionHandlerFeature>();

        if (exception != null)
        {
            Log.Error(
                exception.Error,
                "Erro não tratado");
        }

        context.Response.Redirect("/");
    });
});

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();