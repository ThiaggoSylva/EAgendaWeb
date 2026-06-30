using System.ComponentModel.DataAnnotations;

using EAgendaWeb.WebApp.ModuloTarefa.Dominio;

namespace EAgendaWeb.WebApp.ModuloTarefa.Apresentacao.Models;

public class EditarTarefaViewModel
{
    public Guid Id { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    public PrioridadeTarefaEnum Prioridade { get; set; }
}