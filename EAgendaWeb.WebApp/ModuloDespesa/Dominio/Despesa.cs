using EAgendaWeb.WebApp.Compartilhado.Dominio;
using EAgendaWeb.WebApp.ModuloCategoria.Dominio;

namespace EAgendaWeb.WebApp.ModuloDespesa.Dominio;

public class Despesa : EntidadeBase
{
    public string Descricao { get; set; } = string.Empty;

    public DateTime DataOcorrencia { get; set; }

    public decimal Valor { get; set; }

    public FormaPagamentoEnum FormaPagamento { get; set; }

    public List<Categoria> Categorias { get; set; } = [];
}