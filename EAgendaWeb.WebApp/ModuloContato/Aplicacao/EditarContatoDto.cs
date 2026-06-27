namespace EAgendaWeb.WebApp.ModuloContato.Aplicacao;

public record EditarContatoDto(
    Guid Id,
    string Nome,
    string Email,
    string Telefone,
    string? Cargo,
    string? Empresa
);