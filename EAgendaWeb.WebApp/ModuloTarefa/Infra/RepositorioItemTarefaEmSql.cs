using Dapper;

using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.ModuloTarefa.Dominio;

namespace EAgendaWeb.WebApp.ModuloTarefa.Infra;

public class RepositorioItemTarefaEmSql
    : IRepositorioItemTarefa
{
    private readonly SqlConnectionFactory connectionFactory;

    public RepositorioItemTarefaEmSql(
        SqlConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
    }

    public void Cadastrar(ItemTarefa item)
    {
        using var conexao =
            connectionFactory.CreateConnection();

        string sql = """
            INSERT INTO TBItemTarefa
            (
                Id,
                Titulo,
                Concluido,
                TarefaId
            )
            VALUES
            (
                @Id,
                @Titulo,
                @Concluido,
                @TarefaId
            )
            """;

        conexao.Execute(sql, item);
    }

    public void Excluir(Guid id)
    {
        using var conexao =
            connectionFactory.CreateConnection();

        conexao.Execute(
            """
            DELETE FROM TBItemTarefa
            WHERE Id = @Id
            """,
            new { Id = id });
    }

    public void AtualizarStatus(Guid id)
    {
        using var conexao =
            connectionFactory.CreateConnection();

        conexao.Execute(
            """
            UPDATE TBItemTarefa
            SET Concluido =
                CASE
                    WHEN Concluido = 1 THEN 0
                    ELSE 1
                END
            WHERE Id = @Id
            """,
            new { Id = id });
    }

    public List<ItemTarefa> SelecionarPorTarefa(
        Guid tarefaId)
    {
        using var conexao =
            connectionFactory.CreateConnection();

        return conexao.Query<ItemTarefa>(
            """
            SELECT *
            FROM TBItemTarefa
            WHERE TarefaId = @TarefaId
            """,
            new { TarefaId = tarefaId })
            .ToList();
    }
}