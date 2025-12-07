using System.Data;

namespace CatalogoFilmes.Data;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        // caminho relativo do SQLite
        _connectionString = "Data Source=database.db";
    }

    public IDbConnection CreateConnection()
        => new SqliteConnection(_connectionString);
}