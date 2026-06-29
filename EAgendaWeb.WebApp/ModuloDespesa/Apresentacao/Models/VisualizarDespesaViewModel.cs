using EAgendaWeb.WebApp.ModuloDespesa.Dominio;

namespace EAgendaWeb.WebApp.ModuloDespesa.Apresentacao.Models;

public class VisualizarDespesaViewModel
{
    public Guid Id { get; set; }

    public string Descricao { get; set; }
        = string.Empty;

    public DateTime DataOcorrencia { get; set; }

    public decimal Valor { get; set; }

    public FormaPagamentoEnum FormaPagamento { get; set; }

    public List<string> Categorias { get; set; }
        = [];
}