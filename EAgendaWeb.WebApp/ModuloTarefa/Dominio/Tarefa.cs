using EAgendaWeb.WebApp.Compartilhado.Dominio;

namespace EAgendaWeb.WebApp.ModuloTarefa.Dominio;

public class Tarefa : EntidadeBase
{
    public string Titulo { get; set; } = string.Empty;

    public PrioridadeTarefaEnum Prioridade { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataConclusao { get; set; }

    public bool Concluida { get; set; }

    public int PercentualConcluido { get; set; }

    public List<ItemTarefa> Itens { get; set; } = [];
}