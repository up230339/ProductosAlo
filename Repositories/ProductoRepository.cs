using Microsoft.Data.SqlClient;
using ProductosApi;
using ProductosApi.Data;

namespace ProductosApi.Repositories;

public class ProductoRepository(SqlConnectionFactory connectionFactory) : IProductoRepository
{
    public List<Producto> ObtenerTodos()
    {
        const string sql = "SELECT Id, Nombre, Descripcion, Precio FROM Productos ORDER BY Id";
        var lista = new List<Producto>();

        using var connection = connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();

        while (reader.Read())
            lista.Add(MapearProducto(reader));

        return lista;
    }

    public Producto? ObtenerPorId(int id)
    {
        const string sql = "SELECT Id, Nombre, Descripcion, Precio FROM Productos WHERE Id = @Id";
        using var connection = connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        using var reader = command.ExecuteReader();

        return reader.Read() ? MapearProducto(reader) : null;
    }

    public int Insertar(Producto producto)
    {
        const string sql = @"INSERT INTO Productos (Nombre, Descripcion, Precio)
                             OUTPUT INSERTED.Id
                             VALUES (@Nombre, @Descripcion, @Precio)";
        using var connection = connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AgregarParametros(command, producto);
        connection.Open();
        return (int)command.ExecuteScalar()!;
    }

    public bool Actualizar(Producto producto)
    {
        const string sql = @"UPDATE Productos
                             SET Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio
                             WHERE Id = @Id";
        using var connection = connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        AgregarParametros(command, producto);
        command.Parameters.AddWithValue("@Id", producto.Id);
        connection.Open();
        return command.ExecuteNonQuery() > 0;
    }

    public bool Eliminar(int id)
    {
        const string sql = "DELETE FROM Productos WHERE Id = @Id";
        using var connection = connectionFactory.CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        return command.ExecuteNonQuery() > 0;
    }

    private static void AgregarParametros(SqlCommand command, Producto producto)
    {
        command.Parameters.AddWithValue("@Nombre", producto.Nombre);
        command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
        command.Parameters.AddWithValue("@Precio", producto.Precio);
    }

    private static Producto MapearProducto(SqlDataReader reader) => new()
    {
        Id = reader.GetInt32(0),
        Nombre = reader.GetString(1),
        Descripcion = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
        Precio = reader.GetDecimal(3)
    };
}
