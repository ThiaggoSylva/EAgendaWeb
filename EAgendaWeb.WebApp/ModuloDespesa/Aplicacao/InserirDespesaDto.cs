using EAgendaWeb.WebApp.ModuloDespesa.Dominio;

namespace EAgendaWeb.WebApp.ModuloDespesa.Aplicacao;

public record InserirDespesaDto(
    string Descricao,
    DateTime DataOcorrencia,
    decimal Valor,
    FormaPagamentoEnum FormaPagamento,
    List<Guid> CategoriasIds
);