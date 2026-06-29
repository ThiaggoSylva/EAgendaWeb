using EAgendaWeb.WebApp.Compartilhado.Dominio;

namespace EAgendaWeb.WebApp.ModuloCategoria.Dominio;

public class Categoria : EntidadeBase
{
    public string Titulo { get; set; } = string.Empty;
}