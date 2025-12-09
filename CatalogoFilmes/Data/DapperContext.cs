using System.Data;
using Microsoft.Data.Sqlite;

namespace CatalogoFilmes.Data;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
                            ?? "Data Source=filmes.db";
    }

    public IDbConnection CreateConnection()
        => new SqliteConnection(_connectionString);
}