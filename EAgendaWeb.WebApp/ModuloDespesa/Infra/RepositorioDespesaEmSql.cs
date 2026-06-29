using Dapper;

using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.ModuloCategoria.Dominio;
using EAgendaWeb.WebApp.ModuloDespesa.Dominio;

namespace EAgendaWeb.WebApp.ModuloDespesa.Infra;

public class RepositorioDespesaEmSql : IRepositorioDespesa
{
    private readonly SqlConnectionFactory connectionFactory;

    public RepositorioDespesaEmSql(
        SqlConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
    }

    public void Cadastrar(Despesa despesa)
    {
        using var conexao =
            connectionFactory.CreateConnection();

        using var transacao =
            conexao.BeginTransaction();

        string sqlDespesa = """
            INSERT INTO TBDespesa
            (
                Id,
                Descricao,
                DataOcorrencia,
                Valor,
                FormaPagamento
            )
            VALUES
            (
                @Id,
                @Descricao,
                @DataOcorrencia,
                @Valor,
                @FormaPagamento
            )
            """;

        conexao.Execute(
            sqlDespesa,
            despesa,
            transacao);

        foreach (var categoria in despesa.Categorias)
        {
            string sqlCategoria = """
                INSERT INTO TBDespesaCategoria
                (
                    DespesaId,
                    CategoriaId
                )
                VALUES
                (
                    @DespesaId,
                    @CategoriaId
                )
                """;

            conexao.Execute(
                sqlCategoria,
                new
                {
                    DespesaId = despesa.Id,
                    CategoriaId = categoria.Id
                },
                transacao);
        }

        transacao.Commit();
    }

    public void Editar(Despesa despesa)
    {
        using var conexao =
            connectionFactory.CreateConnection();

        using var transacao =
            conexao.BeginTransaction();

        string sql = """
            UPDATE TBDespesa
            SET
                Descricao = @Descricao,
                DataOcorrencia = @DataOcorrencia,
                Valor = @Valor,
                FormaPagamento = @FormaPagamento
            WHERE Id = @Id
            """;

        conexao.Execute(
            sql,
            despesa,
            transacao);

        conexao.Execute(
            """
            DELETE FROM TBDespesaCategoria
            WHERE DespesaId = @Id
            """,
            new { despesa.Id },
            transacao);

        foreach (var categoria in despesa.Categorias)
        {
            conexao.Execute(
                """
                INSERT INTO TBDespesaCategoria
                (
                    DespesaId,
                    CategoriaId
                )
                VALUES
                (
                    @DespesaId,
                    @CategoriaId
                )
                """,
                new
                {
                    DespesaId = despesa.Id,
                    CategoriaId = categoria.Id
                },
                transacao);
        }

        transacao.Commit();
    }

    public void Excluir(Guid id)
    {
        using var conexao =
            connectionFactory.CreateConnection();

        using var transacao =
            conexao.BeginTransaction();

        conexao.Execute(
            """
            DELETE FROM TBDespesaCategoria
            WHERE DespesaId = @Id
            """,
            new { Id = id },
            transacao);

        conexao.Execute(
            """
            DELETE FROM TBDespesa
            WHERE Id = @Id
            """,
            new { Id = id },
            transacao);

        transacao.Commit();
    }

    public Despesa? SelecionarPorId(Guid id)
    {
        using var conexao =
            connectionFactory.CreateConnection();

        var despesa = conexao.QueryFirstOrDefault<Despesa>(
            """
            SELECT *
            FROM TBDespesa
            WHERE Id = @Id
            """,
            new { Id = id });

        if (despesa is null)
            return null;

        var categorias = conexao.Query<Categoria>(
            """
            SELECT C.*
            FROM TBCategoria C
            INNER JOIN TBDespesaCategoria DC
                ON C.Id = DC.CategoriaId
            WHERE DC.DespesaId = @Id
            """,
            new { Id = id });

        despesa.Categorias =
            categorias.ToList();

        return despesa;
    }

    public List<Despesa> SelecionarTodos()
    {
        using var conexao =
            connectionFactory.CreateConnection();

        var despesas = conexao
            .Query<Despesa>(
                """
                SELECT *
                FROM TBDespesa
                ORDER BY DataOcorrencia DESC
                """)
            .ToList();

        foreach (var despesa in despesas)
        {
            var categorias = conexao.Query<Categoria>(
                """
                SELECT C.*
                FROM TBCategoria C
                INNER JOIN TBDespesaCategoria DC
                    ON C.Id = DC.CategoriaId
                WHERE DC.DespesaId = @Id
                """,
                new { despesa.Id });

            despesa.Categorias =
                categorias.ToList();
        }

        return despesas;
    }
}