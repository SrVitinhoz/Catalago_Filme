using Microsoft.Data.Sqlite;

namespace CatalogoFilmes.Data;

public static class DatabaseInitializer
{
    public static void Initialize(IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection")
                               ?? "Data Source=filmes.db";

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var sql = File.ReadAllText("Data/sql/create_tables.sql");

        using var command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();
    }
}