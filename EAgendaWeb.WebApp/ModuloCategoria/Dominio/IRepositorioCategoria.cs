namespace EAgendaWeb.WebApp.ModuloCategoria.Dominio;

public interface IRepositorioCategoria
{
    void Cadastrar(Categoria categoria);

    void Editar(Categoria categoria);

    void Excluir(Guid id);

    Categoria? SelecionarPorId(Guid id);

    List<Categoria> SelecionarTodos();

    bool ExisteTitulo(string titulo);

    bool ExisteTitulo(Guid id, string titulo);
}