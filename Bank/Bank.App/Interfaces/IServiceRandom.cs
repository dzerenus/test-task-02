namespace Bank.App.Interfaces;

/// <summary>
/// Сервис генерации случайных данных.
/// </summary>
public interface IServiceRandom
{
    /// <summary>
    /// Генерация случайных кошельков.
    /// </summary>
    /// <param name="count">Количество генерируемых кошельков.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task GenerateWallets(
        int count, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Генерация случайных транзакций.
    /// </summary>
    /// <param name="count">Количество генерируемых транзакций для КАЖДОГО кошелька.</param>
    /// <param name="minDate">Минимально возможная дата первой транзакции.</param>
    /// <param name="maxDate">Максимально возможная дата последней транзакции.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task GenerateWalletsTransactions(
        int count, 
        DateTime minDate,
        DateTime maxDate, 
        CancellationToken cancellationToken = default);
}
