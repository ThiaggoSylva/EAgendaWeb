using System.ComponentModel.DataAnnotations;

namespace EAgendaWeb.WebApp.ModuloContato.Apresentacao.Models;

public class EditarContatoViewModel
{
    public Guid Id { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Telefone { get; set; } = string.Empty;

    public string? Cargo { get; set; }

    public string? Empresa { get; set; }
}