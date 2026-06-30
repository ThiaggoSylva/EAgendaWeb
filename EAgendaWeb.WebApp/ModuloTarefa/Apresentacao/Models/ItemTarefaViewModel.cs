namespace EAgendaWeb.WebApp.ModuloTarefa.Apresentacao.Models;

public class ItemTarefaViewModel
{
    public Guid Id { get; set; }

    public string Titulo { get; set; }
        = string.Empty;

    public bool Concluido { get; set; }
}