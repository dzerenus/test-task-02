namespace Bank.Storage;

/// <summary>
/// Конфигурационный файл базы данных.
/// </summary>
public interface IConfigurationStorage
{
    /// <summary>
    /// Строка для подключения к базе данных.
    /// </summary>
    public string StorageConnectionString { get; }
}
