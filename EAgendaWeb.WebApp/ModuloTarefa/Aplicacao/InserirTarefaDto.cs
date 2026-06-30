using EAgendaWeb.WebApp.ModuloTarefa.Dominio;

namespace EAgendaWeb.WebApp.ModuloTarefa.Aplicacao;

public record InserirTarefaDto(
    string Titulo,
    PrioridadeTarefaEnum Prioridade
);