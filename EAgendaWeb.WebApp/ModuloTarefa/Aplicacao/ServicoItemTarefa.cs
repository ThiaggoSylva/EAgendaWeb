using FluentResults;

using EAgendaWeb.WebApp.ModuloTarefa.Dominio;

namespace EAgendaWeb.WebApp.ModuloTarefa.Aplicacao;

public class ServicoItemTarefa
{
    private readonly IRepositorioItemTarefa repositorioItem;
    private readonly IRepositorioTarefa repositorioTarefa;

    public ServicoItemTarefa(
        IRepositorioItemTarefa repositorioItem,
        IRepositorioTarefa repositorioTarefa)
    {
        this.repositorioItem = repositorioItem;
        this.repositorioTarefa = repositorioTarefa;
    }

    public Result AdicionarItem(
        InserirItemTarefaDto dto)
    {
        var item = new ItemTarefa
        {
            TarefaId = dto.TarefaId,
            Titulo = dto.Titulo
        };

        repositorioItem.Cadastrar(item);

        AtualizarPercentual(dto.TarefaId);

        return Result.Ok();
    }

    public void ConcluirItem(Guid itemId, Guid tarefaId)
    {
        repositorioItem.AtualizarStatus(itemId);

        AtualizarPercentual(tarefaId);
    }

    public void RemoverItem(Guid itemId, Guid tarefaId)
    {
        repositorioItem.Excluir(itemId);

        AtualizarPercentual(tarefaId);
    }

    private void AtualizarPercentual(Guid tarefaId)
    {
        var tarefa =
            repositorioTarefa.SelecionarPorId(tarefaId);

        if (tarefa is null)
            return;

        int total = tarefa.Itens.Count;

        if (total == 0)
        {
            tarefa.PercentualConcluido = 0;

            repositorioTarefa.Editar(tarefa);

            return;
        }

        int concluidos =
            tarefa.Itens.Count(i => i.Concluido);

        tarefa.PercentualConcluido =
            (concluidos * 100) / total;

        tarefa.Concluida =
            tarefa.PercentualConcluido == 100;

        if (tarefa.Concluida)
            tarefa.DataConclusao = DateTime.Now;

        repositorioTarefa.Editar(tarefa);
    }
}