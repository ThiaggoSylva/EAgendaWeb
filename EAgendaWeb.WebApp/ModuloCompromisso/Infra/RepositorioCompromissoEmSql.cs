using Dapper;
using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.ModuloCompromisso.Dominio;

namespace EAgendaWeb.WebApp.ModuloCompromisso.Infra;

public class RepositorioCompromissoEmSql : IRepositorioCompromisso
{
    private readonly SqlConnectionFactory connectionFactory;

    public RepositorioCompromissoEmSql(
        SqlConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
    }

    public void Cadastrar(Compromisso compromisso)
    {
        string sql = """
            INSERT INTO TBCompromisso
            (
                Id,
                Assunto,
                DataOcorrencia,
                HoraInicio,
                HoraTermino,
                Tipo,
                Local,
                Link,
                ContatoId
            )
            VALUES
            (
                @Id,
                @Assunto,
                @DataOcorrencia,
                @HoraInicio,
                @HoraTermino,
                @Tipo,
                @Local,
                @Link,
                @ContatoId
            )
            """;

        using var conexao = connectionFactory.CreateConnection();

        conexao.Execute(sql, compromisso);
    }

    public void Editar(Compromisso compromisso)
    {
        string sql = """
            UPDATE TBCompromisso
            SET
                Assunto = @Assunto,
                DataOcorrencia = @DataOcorrencia,
                HoraInicio = @HoraInicio,
                HoraTermino = @HoraTermino,
                Tipo = @Tipo,
                Local = @Local,
                Link = @Link,
                ContatoId = @ContatoId
            WHERE Id = @Id
            """;

        using var conexao = connectionFactory.CreateConnection();

        conexao.Execute(sql, compromisso);
    }

    public void Excluir(Guid id)
    {
        string sql = """
            DELETE FROM TBCompromisso
            WHERE Id = @Id
            """;

        using var conexao = connectionFactory.CreateConnection();

        conexao.Execute(sql, new { Id = id });
    }

    public Compromisso? SelecionarPorId(Guid id)
    {
        string sql = """
            SELECT *
            FROM TBCompromisso
            WHERE Id = @Id
            """;

        using var conexao = connectionFactory.CreateConnection();

        return conexao.QueryFirstOrDefault<Compromisso>(
            sql,
            new { Id = id });
    }

    public List<Compromisso> SelecionarTodos()
    {
        string sql = """
            SELECT *
            FROM TBCompromisso
            ORDER BY DataOcorrencia, HoraInicio
            """;

        using var conexao = connectionFactory.CreateConnection();

        return conexao.Query<Compromisso>(sql)
            .ToList();
    }

    public bool ExisteConflitoHorario(
        Guid? compromissoId,
        DateOnly data,
        TimeOnly inicio,
        TimeOnly termino)
    {
        string sql = """
            SELECT COUNT(*)
            FROM TBCompromisso
            WHERE DataOcorrencia = @Data
            AND Id <> ISNULL(@CompromissoId, '00000000-0000-0000-0000-000000000000')
            AND (
                @Inicio < HoraTermino
                AND
                @Termino > HoraInicio
            )
            """;

        using var conexao = connectionFactory.CreateConnection();

        int quantidade = conexao.ExecuteScalar<int>(
            sql,
            new
            {
                CompromissoId = compromissoId,
                Data = data,
                Inicio = inicio,
                Termino = termino
            });

        return quantidade > 0;
    }
}