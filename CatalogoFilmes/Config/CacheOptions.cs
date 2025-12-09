namespace CatalogoFilmes.Config;

public class CacheOptions
{
    // Expiração padrão do cache (em segundos)
    public int DefaultExpirationSeconds { get; set; } = 300; // 5 min

    // Prefixo para evitar colisao de chaves
    public string KeyPrefix { get; set; } = "catalogo_";

    // Se o cache está habilitado
    public bool Enabled { get; set; } = true;
}