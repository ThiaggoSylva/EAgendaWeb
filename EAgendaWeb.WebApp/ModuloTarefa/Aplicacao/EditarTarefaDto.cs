using EAgendaWeb.WebApp.ModuloTarefa.Dominio;

namespace EAgendaWeb.WebApp.ModuloTarefa.Aplicacao;

public record EditarTarefaDto(
    Guid Id,
    string Titulo,
    PrioridadeTarefaEnum Prioridade
);