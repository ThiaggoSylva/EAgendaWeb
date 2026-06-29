using Dapper;

using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.ModuloCategoria.Dominio;

namespace EAgendaWeb.WebApp.ModuloCategoria.Infra;

public class RepositorioCategoriaEmSql : IRepositorioCategoria
{
    private readonly SqlConnectionFactory connectionFactory;

    public RepositorioCategoriaEmSql(
        SqlConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
    }

    public void Cadastrar(Categoria categoria)
    {
        string sql = """
            INSERT INTO TBCategoria
            (
                Id,
                Titulo
            )
            VALUES
            (
                @Id,
                @Titulo
            )
            """;

        using var conexao =
            connectionFactory.CreateConnection();

        conexao.Execute(sql, categoria);
    }

    public void Editar(Categoria categoria)
    {
        string sql = """
            UPDATE TBCategoria
            SET
                Titulo = @Titulo
            WHERE Id = @Id
            """;

        using var conexao =
            connectionFactory.CreateConnection();

        conexao.Execute(sql, categoria);
    }

    public void Excluir(Guid id)
    {
        string sql = """
            DELETE FROM TBCategoria
            WHERE Id = @Id
            """;

        using var conexao =
            connectionFactory.CreateConnection();

        conexao.Execute(sql, new { Id = id });
    }

    public Categoria? SelecionarPorId(Guid id)
    {
        string sql = """
            SELECT *
            FROM TBCategoria
            WHERE Id = @Id
            """;

        using var conexao =
            connectionFactory.CreateConnection();

        return conexao.QueryFirstOrDefault<Categoria>(
            sql,
            new { Id = id });
    }

    public List<Categoria> SelecionarTodos()
    {
        string sql = """
            SELECT *
            FROM TBCategoria
            ORDER BY Titulo
            """;

        using var conexao =
            connectionFactory.CreateConnection();

        return conexao.Query<Categoria>(sql)
            .ToList();
    }

    public bool ExisteTitulo(string titulo)
    {
        string sql = """
            SELECT COUNT(*)
            FROM TBCategoria
            WHERE Titulo = @Titulo
            """;

        using var conexao =
            connectionFactory.CreateConnection();

        int quantidade =
            conexao.ExecuteScalar<int>(
                sql,
                new { Titulo = titulo });

        return quantidade > 0;
    }

    public bool ExisteTitulo(
        Guid id,
        string titulo)
    {
        string sql = """
            SELECT COUNT(*)
            FROM TBCategoria
            WHERE Titulo = @Titulo
            AND Id <> @Id
            """;

        using var conexao =
            connectionFactory.CreateConnection();

        int quantidade =
            conexao.ExecuteScalar<int>(
                sql,
                new
                {
                    Id = id,
                    Titulo = titulo
                });

        return quantidade > 0;
    }
    public bool PossuiDespesas(Guid categoriaId)
    {
    using var conexao =
        connectionFactory.CreateConnection();

    string sql = """
        SELECT COUNT(*)
        FROM TBDespesaCategoria
        WHERE CategoriaId = @CategoriaId
        """;

    int quantidade = conexao.ExecuteScalar<int>(
        sql,
        new { CategoriaId = categoriaId });

    return quantidade > 0;
    }
}