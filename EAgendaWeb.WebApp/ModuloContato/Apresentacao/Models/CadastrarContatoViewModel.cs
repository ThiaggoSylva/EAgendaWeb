using System.ComponentModel.DataAnnotations;

namespace EAgendaWeb.WebApp.ModuloContato.Apresentacao.Models;

public class CadastrarContatoViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MinLength(2)]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    public string Telefone { get; set; } = string.Empty;

    public string? Cargo { get; set; }

    public string? Empresa { get; set; }
}