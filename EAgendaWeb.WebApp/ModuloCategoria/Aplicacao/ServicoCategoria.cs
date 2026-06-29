using FluentResults;
using EAgendaWeb.WebApp.ModuloCategoria.Dominio;

namespace EAgendaWeb.WebApp.ModuloCategoria.Aplicacao;

public class ServicoCategoria
{
    private readonly IRepositorioCategoria repositorio;
    private readonly ValidadorCategoria validador;

    public ServicoCategoria(
        IRepositorioCategoria repositorio)
    {
        this.repositorio = repositorio;

        validador = new ValidadorCategoria();
    }

    public Result Cadastrar(InserirCategoriaDto dto)
    {
        var categoria = new Categoria
        {
            Titulo = dto.Titulo
        };

        var erros = validador.Validar(categoria);

        if (erros.Any())
            return Result.Fail(erros);

        if (repositorio.ExisteTitulo(dto.Titulo))
            return Result.Fail(
                "Já existe uma categoria com este título.");

        repositorio.Cadastrar(categoria);

        return Result.Ok();
    }

    public Result Editar(EditarCategoriaDto dto)
    {
        var categoria =
            repositorio.SelecionarPorId(dto.Id);

        if (categoria is null)
            return Result.Fail(
                "Categoria não encontrada.");

        categoria.Titulo = dto.Titulo;

        var erros = validador.Validar(categoria);

        if (erros.Any())
            return Result.Fail(erros);

        if (repositorio.ExisteTitulo(
                categoria.Id,
                categoria.Titulo))
        {
            return Result.Fail(
                "Já existe uma categoria com este título.");
        }

        repositorio.Editar(categoria);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        var categoria =
            repositorio.SelecionarPorId(id);

        if (categoria is null)
            return Result.Fail(
                "Categoria não encontrada.");

        if (repositorio.PossuiDespesas(id))
        {
            return Result.Fail(
                "A categoria possui despesas vinculadas.");
        }

        repositorio.Excluir(id);

        return Result.Ok();
    }

    public DetalhesCategoriaDto? SelecionarPorId(Guid id)
    {
        var categoria =
            repositorio.SelecionarPorId(id);

        if (categoria is null)
            return null;

        return new DetalhesCategoriaDto(
            categoria.Id,
            categoria.Titulo
        );
    }

    public List<ListarCategoriaDto> SelecionarTodos()
    {
        var categorias =
            repositorio.SelecionarTodos();

        return categorias
            .Select(c => new ListarCategoriaDto(
                c.Id,
                c.Titulo))
            .ToList();
    }
}