using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

using EAgendaWeb.WebApp.ModuloDespesa.Dominio;

namespace EAgendaWeb.WebApp.ModuloDespesa.Apresentacao.Models;

public class EditarDespesaViewModel
{
    public Guid Id { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public DateTime DataOcorrencia { get; set; }

    [Required]
    public decimal Valor { get; set; }

    [Required]
    public FormaPagamentoEnum FormaPagamento { get; set; }

    [Required]
    public List<Guid> CategoriasIds { get; set; }
        = [];

    public MultiSelectList? CategoriasDisponiveis { get; set; }
}