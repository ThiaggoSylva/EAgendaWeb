namespace EAgendaWeb.WebApp.ModuloTarefa.Dominio;

public interface IRepositorioTarefa
{
    void Cadastrar(Tarefa tarefa);

    void Editar(Tarefa tarefa);

    void Excluir(Guid id);

    Tarefa? SelecionarPorId(Guid id);

    List<Tarefa> SelecionarTodos();

    List<Tarefa> SelecionarPendentes();

    List<Tarefa> SelecionarConcluidas();
}