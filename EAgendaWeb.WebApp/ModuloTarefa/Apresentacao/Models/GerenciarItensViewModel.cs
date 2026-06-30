namespace EAgendaWeb.WebApp.ModuloTarefa.Apresentacao.Models;

public class GerenciarItensViewModel
{
    public Guid TarefaId { get; set; }

    public string TituloTarefa { get; set; }
        = string.Empty;

    public int PercentualConcluido { get; set; }

    public string NovoItem { get; set; }
        = string.Empty;

    public List<ItemTarefaViewModel> Itens { get; set; }
        = [];
}