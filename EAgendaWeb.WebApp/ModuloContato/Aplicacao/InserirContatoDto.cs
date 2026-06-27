namespace EAgendaWeb.WebApp.ModuloContato.Aplicacao;

public record InserirContatoDto(
    string Nome,
    string Email,
    string Telefone,
    string? Cargo,
    string? Empresa
);