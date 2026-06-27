namespace EAgendaWeb.WebApp.Compartilhado.Dominio;

public abstract class EntidadeBase
{
    public Guid Id { get; set; }

    protected EntidadeBase()
    {
        Id = Guid.NewGuid();
    }
}