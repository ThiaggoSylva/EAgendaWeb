using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

using EAgendaWeb.WebApp.ModuloDespesa.Dominio;

namespace EAgendaWeb.WebApp.ModuloDespesa.Apresentacao.Models;

public class CadastrarDespesaViewModel
{
    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public DateTime DataOcorrencia { get; set; }
        = DateTime.Today;

    [Required]
    public decimal Valor { get; set; }

    [Required]
    public FormaPagamentoEnum FormaPagamento { get; set; }

    [Required]
    public List<Guid> CategoriasIds { get; set; }
        = [];

    public MultiSelectList? CategoriasDisponiveis { get; set; }
}