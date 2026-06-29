namespace EAgendaWeb.WebApp.ModuloDespesa.Dominio;

public interface IRepositorioDespesa
{
    void Cadastrar(Despesa despesa);

    void Editar(Despesa despesa);

    void Excluir(Guid id);

    Despesa? SelecionarPorId(Guid id);

    List<Despesa> SelecionarTodos();
}