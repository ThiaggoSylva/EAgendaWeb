namespace EAgendaWeb.WebApp.ModuloTarefa.Dominio;

public interface IRepositorioItemTarefa
{
    void Cadastrar(ItemTarefa item);

    void Excluir(Guid id);

    void AtualizarStatus(Guid id);

    List<ItemTarefa> SelecionarPorTarefa(Guid tarefaId);
}