namespace EAgendaWeb.WebApp.ModuloCompromisso.Dominio;

public interface IRepositorioCompromisso
{
    void Cadastrar(Compromisso compromisso);

    void Editar(Compromisso compromisso);

    void Excluir(Guid id);

    Compromisso? SelecionarPorId(Guid id);

    List<Compromisso> SelecionarTodos();

    bool ExisteConflitoHorario(
        Guid? compromissoId,
        DateOnly data,
        TimeOnly inicio,
        TimeOnly termino);

    bool ExisteCompromissoParaContato(Guid contatoId);
}