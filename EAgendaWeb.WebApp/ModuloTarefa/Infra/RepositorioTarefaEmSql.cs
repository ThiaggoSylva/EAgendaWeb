using Dapper;

using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.ModuloTarefa.Dominio;

namespace EAgendaWeb.WebApp.ModuloTarefa.Infra;

public class RepositorioTarefaEmSql : IRepositorioTarefa
{
    private readonly SqlConnectionFactory connectionFactory;

    public RepositorioTarefaEmSql(
        SqlConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
    }

    public void Cadastrar(Tarefa tarefa)
    {
        using var conexao =
            connectionFactory.CreateConnection();

        string sql = """
            INSERT INTO TBTarefa
            (
                Id,
                Titulo,
                Prioridade,
                DataCriacao,
                DataConclusao,
                Concluida,
                PercentualConcluido
            )
            VALUES
            (
                @Id,
                @Titulo,
                @Prioridade,
                @DataCriacao,
                @DataConclusao,
                @Concluida,
                @PercentualConcluido
            )
            """;

        conexao.Execute(sql, tarefa);
    }

    public void Editar(Tarefa tarefa)
    {
        using var conexao =
            connectionFactory.CreateConnection();

        string sql = """
            UPDATE TBTarefa
            SET
                Titulo = @Titulo,
                Prioridade = @Prioridade,
                DataConclusao = @DataConclusao,
                Concluida = @Concluida,
                PercentualConcluido = @PercentualConcluido
            WHERE Id = @Id
            """;

        conexao.Execute(sql, tarefa);
    }

    public void Excluir(Guid id)
    {
        using var conexao =
            connectionFactory.CreateConnection();

        conexao.Execute(
            "DELETE FROM TBItemTarefa WHERE TarefaId = @Id",
            new { Id = id });

        conexao.Execute(
            "DELETE FROM TBTarefa WHERE Id = @Id",
            new { Id = id });
    }

    public Tarefa? SelecionarPorId(Guid id)
    {
        using var conexao =
            connectionFactory.CreateConnection();

        var tarefa = conexao.QueryFirstOrDefault<Tarefa>(
            """
            SELECT *
            FROM TBTarefa
            WHERE Id = @Id
            """,
            new { Id = id });

        if (tarefa is null)
            return null;

        tarefa.Itens = conexao.Query<ItemTarefa>(
            """
            SELECT *
            FROM TBItemTarefa
            WHERE TarefaId = @Id
            """,
            new { Id = id }).ToList();

        return tarefa;
    }

    public List<Tarefa> SelecionarTodos()
    {
        using var conexao =
            connectionFactory.CreateConnection();

        var tarefas = conexao.Query<Tarefa>(
            """
            SELECT *
            FROM TBTarefa
            ORDER BY Prioridade DESC
            """).ToList();

        foreach (var tarefa in tarefas)
        {
            tarefa.Itens = conexao.Query<ItemTarefa>(
                """
                SELECT *
                FROM TBItemTarefa
                WHERE TarefaId = @Id
                """,
                new { Id = tarefa.Id }).ToList();
        }

        return tarefas;
    }

    public List<Tarefa> SelecionarPendentes()
    {
        using var conexao =
            connectionFactory.CreateConnection();

        return conexao.Query<Tarefa>(
            """
            SELECT *
            FROM TBTarefa
            WHERE Concluida = 0
            ORDER BY Prioridade DESC
            """).ToList();
    }

    public List<Tarefa> SelecionarConcluidas()
    {
        using var conexao =
            connectionFactory.CreateConnection();

        return conexao.Query<Tarefa>(
            """
            SELECT *
            FROM TBTarefa
            WHERE Concluida = 1
            ORDER BY DataConclusao DESC
            """).ToList();
    }
}