using EAgendaWeb.WebApp.ModuloTarefa.Dominio;

namespace EAgendaWeb.WebApp.ModuloTarefa.Apresentacao.Models;

public class VisualizarTarefaViewModel
{
    public Guid Id { get; set; }

    public string Titulo { get; set; }
        = string.Empty;

    public PrioridadeTarefaEnum Prioridade { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataConclusao { get; set; }

    public int PercentualConcluido { get; set; }

    public bool Concluida { get; set; }
}