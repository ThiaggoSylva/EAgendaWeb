using FluentResults;
using EAgendaWeb.WebApp.ModuloContato.Dominio;
using EAgendaWeb.WebApp.ModuloCompromisso.Dominio;

namespace EAgendaWeb.WebApp.ModuloContato.Aplicacao;

public class ServicoContato
{
    private readonly IRepositorioContato repositorio;

    private readonly IRepositorioCompromisso repositorioCompromisso;
    private readonly ValidadorContato validador;

    public ServicoContato(
        IRepositorioContato repositorio,
        IRepositorioCompromisso repositorioCompromisso)
    {
        this.repositorio = repositorio;
        this.repositorioCompromisso = repositorioCompromisso;

        validador = new ValidadorContato();
    }

    public Result Cadastrar(InserirContatoDto dto)
    {
        var contato = new Contato(
            dto.Nome,
            dto.Email,
            dto.Telefone,
            dto.Cargo,
            dto.Empresa);

        var erros = validador.Validar(contato);

        if (erros.Any())
            return Result.Fail(erros);

        if (repositorio.EmailJaExiste(dto.Email))
            return Result.Fail("Já existe um contato com este e-mail.");

        if (repositorio.TelefoneJaExiste(dto.Telefone))
            return Result.Fail("Já existe um contato com este telefone.");

        repositorio.Cadastrar(contato);

        return Result.Ok();
    }

    public Result Editar(EditarContatoDto dto)
    {
        var contato = repositorio.SelecionarPorId(dto.Id);

        if (contato is null)
            return Result.Fail("Contato não encontrado.");

        contato.Nome = dto.Nome;
        contato.Email = dto.Email;
        contato.Telefone = dto.Telefone;
        contato.Cargo = dto.Cargo;
        contato.Empresa = dto.Empresa;

        var erros = validador.Validar(contato);

        if (erros.Any())
            return Result.Fail(erros);

        repositorio.Editar(contato);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
    var contato = repositorio.SelecionarPorId(id);

    if (contato is null)
        return Result.Fail("Contato não encontrado.");

    bool possuiCompromissos =
        repositorioCompromisso
            .ExisteCompromissoParaContato(id);

    if (possuiCompromissos)
    {
        return Result.Fail(
            "O contato possui compromissos vinculados e não pode ser excluído.");
    }

    repositorio.Excluir(id);

    return Result.Ok();
    }

    public DetalhesContatoDto? SelecionarPorId(Guid id)
    {
        var contato = repositorio.SelecionarPorId(id);

        if (contato is null)
            return null;

        return new DetalhesContatoDto(
            contato.Id,
            contato.Nome,
            contato.Email,
            contato.Telefone,
            contato.Cargo,
            contato.Empresa);
    }

    public List<ListarContatoDto> SelecionarTodos()
    {
        var contatos = repositorio.SelecionarTodos();

        return contatos
            .Select(x => new ListarContatoDto(
                x.Id,
                x.Nome,
                x.Email,
                x.Telefone,
                x.Cargo,
                x.Empresa))
            .ToList();
    }
}