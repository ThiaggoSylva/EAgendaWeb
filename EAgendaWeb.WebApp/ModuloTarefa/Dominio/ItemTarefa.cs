using EAgendaWeb.WebApp.Compartilhado.Dominio;

namespace EAgendaWeb.WebApp.ModuloTarefa.Dominio;

public class ItemTarefa : EntidadeBase
{
    public string Titulo { get; set; } = string.Empty;

    public bool Concluido { get; set; }

    public Guid TarefaId { get; set; }
}