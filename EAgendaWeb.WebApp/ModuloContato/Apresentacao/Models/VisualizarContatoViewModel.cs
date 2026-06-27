namespace EAgendaWeb.WebApp.ModuloContato.Apresentacao.Models;

public class VisualizarContatoViewModel
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string? Cargo { get; set; }

    public string? Empresa { get; set; }
}