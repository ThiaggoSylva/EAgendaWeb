using System.Data;
using Microsoft.Data.SqlClient;

namespace EAgendaWeb.WebApp.Compartilhado.Infra.Sql;

public class SqlConnectionFactory
{
    private readonly IConfiguration configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        string connectionString =
            configuration.GetConnectionString("SqlServer")!;

        return new SqlConnection(connectionString);
    }
}