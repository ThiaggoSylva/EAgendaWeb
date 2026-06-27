using Dapper;
using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.ModuloContato.Dominio;

namespace EAgendaWeb.WebApp.ModuloContato.Infra;

public class RepositorioContatoEmSql : IRepositorioContato
{
    private readonly SqlConnectionFactory connectionFactory;

    public RepositorioContatoEmSql(
        SqlConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
    }

    public void Cadastrar(Contato contato)
    {
        string sql = @"
            INSERT INTO TBContato
            (
                Id,
                Nome,
                Email,
                Telefone,
                Cargo,
                Empresa
            )
            VALUES
            (
                @Id,
                @Nome,
                @Email,
                @Telefone,
                @Cargo,
                @Empresa
            );";

        using var conexao = connectionFactory.CreateConnection();

        conexao.Execute(sql, contato);
    }

    public void Editar(Contato contato)
    {
        string sql = @"
            UPDATE TBContato
            SET
                Nome = @Nome,
                Email = @Email,
                Telefone = @Telefone,
                Cargo = @Cargo,
                Empresa = @Empresa
            WHERE Id = @Id";

        using var conexao = connectionFactory.CreateConnection();

        conexao.Execute(sql, contato);
    }

    public void Excluir(Guid id)
    {
        string sql = """
            DELETE FROM TBContato
            WHERE Id = @Id
            """;

        using var conexao = connectionFactory.CreateConnection();

        conexao.Execute(sql, new { Id = id });
    }

    public Contato? SelecionarPorId(Guid id)
    {
        string sql = """
            SELECT *
            FROM TBContato
            WHERE Id = @Id
            """;

        using var conexao = connectionFactory.CreateConnection();

        return conexao.QueryFirstOrDefault<Contato>(
            sql,
            new { Id = id });
    }

    public List<Contato> SelecionarTodos()
    {
        string sql = """
            SELECT *
            FROM TBContato
            ORDER BY Nome
            """;

        using var conexao = connectionFactory.CreateConnection();

        return conexao.Query<Contato>(sql)
            .ToList();
    }

    public bool EmailJaExiste(string email)
    {
        string sql = """
            SELECT COUNT(*)
            FROM TBContato
            WHERE Email = @Email
            """;

        using var conexao = connectionFactory.CreateConnection();

        return conexao.ExecuteScalar<int>(
            sql,
            new { Email = email }) > 0;
    }

    public bool TelefoneJaExiste(string telefone)
    {
        string sql = """
            SELECT COUNT(*)
            FROM TBContato
            WHERE Telefone = @Telefone
            """;

        using var conexao = connectionFactory.CreateConnection();

        return conexao.ExecuteScalar<int>(
            sql,
            new { Telefone = telefone }) > 0;
    }
}