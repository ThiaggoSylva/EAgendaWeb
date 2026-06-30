using FluentResults;

using EAgendaWeb.WebApp.ModuloTarefa.Dominio;

namespace EAgendaWeb.WebApp.ModuloTarefa.Aplicacao;

public class ServicoTarefa
{
    private readonly IRepositorioTarefa repositorio;
    private readonly ValidadorTarefa validador;

    public ServicoTarefa(
        IRepositorioTarefa repositorio)
    {
        this.repositorio = repositorio;

        validador = new ValidadorTarefa();
    }

    public Result Cadastrar(InserirTarefaDto dto)
    {
        var tarefa = new Tarefa
        {
            Titulo = dto.Titulo,
            Prioridade = dto.Prioridade,
            DataCriacao = DateTime.Now,
            PercentualConcluido = 0,
            Concluida = false
        };

        var erros = validador.Validar(tarefa);

        if (erros.Any())
            return Result.Fail(erros);

        repositorio.Cadastrar(tarefa);

        return Result.Ok();
    }

    public Result Editar(EditarTarefaDto dto)
    {
        var tarefa =
            repositorio.SelecionarPorId(dto.Id);

        if (tarefa is null)
            return Result.Fail("Tarefa não encontrada.");

        tarefa.Titulo = dto.Titulo;
        tarefa.Prioridade = dto.Prioridade;

        var erros = validador.Validar(tarefa);

        if (erros.Any())
            return Result.Fail(erros);

        repositorio.Editar(tarefa);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        repositorio.Excluir(id);

        return Result.Ok();
    }

    public List<ListarTarefaDto> SelecionarTodos()
    {
        return repositorio
            .SelecionarTodos()
            .Select(t => new ListarTarefaDto(
                t.Id,
                t.Titulo,
                t.Prioridade,
                t.PercentualConcluido,
                t.Concluida))
            .ToList();
    }

    public List<ListarTarefaDto> SelecionarPendentes()
    {
        return repositorio
            .SelecionarPendentes()
            .Select(t => new ListarTarefaDto(
                t.Id,
                t.Titulo,
                t.Prioridade,
                t.PercentualConcluido,
                t.Concluida))
            .ToList();
    }

    public List<ListarTarefaDto> SelecionarConcluidas()
    {
        return repositorio
            .SelecionarConcluidas()
            .Select(t => new ListarTarefaDto(
                t.Id,
                t.Titulo,
                t.Prioridade,
                t.PercentualConcluido,
                t.Concluida))
            .ToList();
    }

    public DetalhesTarefaDto? SelecionarPorId(Guid id)
    {
        var tarefa = repositorio.SelecionarPorId(id);

        if (tarefa is null)
            return null;

        return new DetalhesTarefaDto(
            tarefa.Id,
            tarefa.Titulo,
            tarefa.Prioridade,
            tarefa.DataCriacao,
            tarefa.DataConclusao,
            tarefa.PercentualConcluido,
            tarefa.Concluida
        );
    }

    public List<ListarTarefaDto> SelecionarPorPrioridade(
    PrioridadeTarefaEnum prioridade)
    {
        return repositorio
        .SelecionarTodos()
        .Where(x => x.Prioridade == prioridade)
        .Select(t => new ListarTarefaDto(
            t.Id,
            t.Titulo,
            t.Prioridade,
            t.PercentualConcluido,
            t.Concluida))
        .ToList();
    }
}