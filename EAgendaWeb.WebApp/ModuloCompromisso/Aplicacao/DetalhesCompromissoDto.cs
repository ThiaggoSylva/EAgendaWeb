using EAgendaWeb.WebApp.ModuloCompromisso.Dominio;

namespace EAgendaWeb.WebApp.ModuloCompromisso.Aplicacao;

public record DetalhesCompromissoDto(
    Guid Id,
    string Assunto,
    DateOnly DataOcorrencia,
    TimeOnly HoraInicio,
    TimeOnly HoraTermino,
    TipoCompromissoEnum Tipo,
    string? Local,
    string? Link,
    Guid? ContatoId
);