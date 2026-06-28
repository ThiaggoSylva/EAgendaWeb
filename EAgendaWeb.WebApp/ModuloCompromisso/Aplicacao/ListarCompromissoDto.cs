using EAgendaWeb.WebApp.ModuloCompromisso.Dominio;

namespace EAgendaWeb.WebApp.ModuloCompromisso.Aplicacao;

public record ListarCompromissoDto(
    Guid Id,
    string Assunto,
    DateOnly DataOcorrencia,
    TimeOnly HoraInicio,
    TimeOnly HoraTermino,
    TipoCompromissoEnum Tipo
);