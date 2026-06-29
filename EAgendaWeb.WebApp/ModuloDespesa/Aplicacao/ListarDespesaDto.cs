using EAgendaWeb.WebApp.ModuloDespesa.Dominio;

namespace EAgendaWeb.WebApp.ModuloDespesa.Aplicacao;

public record ListarDespesaDto(
    Guid Id,
    string Descricao,
    DateTime DataOcorrencia,
    decimal Valor,
    FormaPagamentoEnum FormaPagamento,
    string Categorias
);