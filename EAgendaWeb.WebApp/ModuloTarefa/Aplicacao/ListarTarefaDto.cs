using EAgendaWeb.WebApp.ModuloTarefa.Dominio;

namespace EAgendaWeb.WebApp.ModuloTarefa.Aplicacao;

public record ListarTarefaDto(
    Guid Id,
    string Titulo,
    PrioridadeTarefaEnum Prioridade,
    int PercentualConcluido,
    bool Concluida
);