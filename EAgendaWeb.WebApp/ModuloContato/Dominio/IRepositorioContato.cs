namespace EAgendaWeb.WebApp.ModuloContato.Dominio;

public interface IRepositorioContato
{
    void Cadastrar(Contato contato);

    void Editar(Contato contato);

    void Excluir(Guid id);

    Contato? SelecionarPorId(Guid id);

    List<Contato> SelecionarTodos();

    bool EmailJaExiste(string email);

    bool TelefoneJaExiste(string telefone);
}