using EAgendaWeb.WebApp.ModuloTarefa.Dominio;

namespace EAgendaWeb.WebApp.ModuloTarefa.Aplicacao;

public record DetalhesTarefaDto(
    Guid Id,
    string Titulo,
    PrioridadeTarefaEnum Prioridade,
    DateTime DataCriacao,
    DateTime? DataConclusao,
    int PercentualConcluido,
    bool Concluida
);