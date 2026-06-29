using System.ComponentModel.DataAnnotations;

namespace EAgendaWeb.WebApp.ModuloCategoria.Apresentacao.Models;

public class CadastrarCategoriaViewModel
{
    [Required(ErrorMessage = "O campo Título é obrigatório.")]
    [MinLength(2)]
    [MaxLength(100)]
    public string Titulo { get; set; } = string.Empty;
}