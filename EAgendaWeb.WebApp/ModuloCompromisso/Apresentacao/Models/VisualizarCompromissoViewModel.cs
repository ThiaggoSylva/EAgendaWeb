using EAgendaWeb.WebApp.ModuloCompromisso.Dominio;

namespace EAgendaWeb.WebApp.ModuloCompromisso.Apresentacao.Models;

public class VisualizarCompromissoViewModel
{
    public Guid Id { get; set; }

    public string Assunto { get; set; } = string.Empty;

    public DateOnly DataOcorrencia { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraTermino { get; set; }

    public TipoCompromissoEnum Tipo { get; set; }

    public string? Local { get; set; }

    public string? Link { get; set; }

    public Guid? ContatoId { get; set; }
}