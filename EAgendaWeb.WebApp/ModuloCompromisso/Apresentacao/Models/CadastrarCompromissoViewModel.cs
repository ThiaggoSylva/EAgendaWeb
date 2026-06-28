using System.ComponentModel.DataAnnotations;
using EAgendaWeb.WebApp.ModuloCompromisso.Dominio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EAgendaWeb.WebApp.ModuloCompromisso.Apresentacao.Models;

public class CadastrarCompromissoViewModel
{
    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Assunto { get; set; } = string.Empty;

    [Required]
    public DateOnly DataOcorrencia { get; set; }

    [Required]
    public TimeOnly HoraInicio { get; set; }

    [Required]
    public TimeOnly HoraTermino { get; set; }

    [Required]
    public TipoCompromissoEnum Tipo { get; set; }

    public string? Local { get; set; }

    public string? Link { get; set; }

    public List<SelectListItem> ContatosDisponiveis { get; set; } = [];

    public Guid? ContatoId { get; set; }
}