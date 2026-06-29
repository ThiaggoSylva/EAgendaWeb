using FluentResults;

using EAgendaWeb.WebApp.ModuloCategoria.Dominio;
using EAgendaWeb.WebApp.ModuloDespesa.Dominio;

namespace EAgendaWeb.WebApp.ModuloDespesa.Aplicacao;

public class ServicoDespesa
{
    private readonly IRepositorioDespesa repositorioDespesa;
    private readonly IRepositorioCategoria repositorioCategoria;
    private readonly ValidadorDespesa validador;

    public ServicoDespesa(
        IRepositorioDespesa repositorioDespesa,
        IRepositorioCategoria repositorioCategoria)
    {
        this.repositorioDespesa = repositorioDespesa;
        this.repositorioCategoria = repositorioCategoria;

        validador = new ValidadorDespesa();
    }

    public Result Cadastrar(InserirDespesaDto dto)
    {
        var categorias = dto.CategoriasIds
            .Select(id => repositorioCategoria.SelecionarPorId(id))
            .Where(c => c is not null)
            .ToList();

        var despesa = new Despesa
        {
            Descricao = dto.Descricao,
            DataOcorrencia = dto.DataOcorrencia,
            Valor = dto.Valor,
            FormaPagamento = dto.FormaPagamento,
            Categorias = categorias!
        };

        var erros = validador.Validar(despesa);

        if (erros.Any())
            return Result.Fail(erros);

        repositorioDespesa.Cadastrar(despesa);

        return Result.Ok();
    }

    public Result Editar(EditarDespesaDto dto)
    {
        var despesa =
            repositorioDespesa.SelecionarPorId(dto.Id);

        if (despesa is null)
            return Result.Fail("Despesa não encontrada.");

        despesa.Descricao = dto.Descricao;
        despesa.DataOcorrencia = dto.DataOcorrencia;
        despesa.Valor = dto.Valor;
        despesa.FormaPagamento = dto.FormaPagamento;

        despesa.Categorias = dto.CategoriasIds
            .Select(id => repositorioCategoria.SelecionarPorId(id))
            .Where(c => c is not null)
            .ToList()!;

        var erros = validador.Validar(despesa);

        if (erros.Any())
            return Result.Fail(erros);

        repositorioDespesa.Editar(despesa);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        var despesa =
            repositorioDespesa.SelecionarPorId(id);

        if (despesa is null)
            return Result.Fail("Despesa não encontrada.");

        repositorioDespesa.Excluir(id);

        return Result.Ok();
    }

    public DetalhesDespesaDto? SelecionarPorId(Guid id)
    {
        var despesa =
            repositorioDespesa.SelecionarPorId(id);

        if (despesa is null)
            return null;

        return new DetalhesDespesaDto(
            despesa.Id,
            despesa.Descricao,
            despesa.DataOcorrencia,
            despesa.Valor,
            despesa.FormaPagamento,
            despesa.Categorias.Select(c => c.Id).ToList()
        );
    }

    public List<ListarDespesaDto> SelecionarTodos()
    {
        var despesas =
            repositorioDespesa.SelecionarTodos();

        return despesas
            .Select(d => new ListarDespesaDto(
                d.Id,
                d.Descricao,
                d.DataOcorrencia,
                d.Valor,
                d.FormaPagamento,
                string.Join(", ",
                    d.Categorias.Select(c => c.Titulo))
            ))
            .ToList();
    }
}