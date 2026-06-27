using EAgendaWeb.WebApp.Compartilhado.Dominio;

namespace EAgendaWeb.WebApp.ModuloContato.Dominio;

public class Contato : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string? Cargo { get; set; }

    public string? Empresa { get; set; }

    public Contato()
    {
    }

    public Contato(
        string nome,
        string email,
        string telefone,
        string? cargo,
        string? empresa)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Cargo = cargo;
        Empresa = empresa;
    }
}