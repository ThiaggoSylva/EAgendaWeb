using System.ComponentModel.DataAnnotations;

using EAgendaWeb.WebApp.ModuloTarefa.Dominio;

namespace EAgendaWeb.WebApp.ModuloTarefa.Apresentacao.Models;

public class CadastrarTarefaViewModel
{
    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    public PrioridadeTarefaEnum Prioridade { get; set; }
}