using Microsoft.Data.SqlClient;

namespace ProductosApi.Data;

public class SqlConnectionFactory(IConfiguration configuration)
{
    private readonly string _connectionString =
        configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("No se encontró DefaultConnection.");

    public SqlConnection CreateConnection() => new(_connectionString);
}
