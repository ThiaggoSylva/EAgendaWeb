namespace EAgendaWeb.WebApp.ModuloContato.Aplicacao;

public record DetalhesContatoDto(
    Guid Id,
    string Nome,
    string Email,
    string Telefone,
    string? Cargo,
    string? Empresa
);