using Bank.Core.Models;
using Bank.Storage;
using Bank.App.Interfaces;

namespace Bank.App.Services;

/// <summary>
/// Сервис управления транзакциями.
/// </summary>
/// <param name="storage">Сервис взаимодейсвтия с хранилищем.</param>
internal class ServiceTransaction(IServiceStorage storage) : IServiceTransaction
{
    /// <summary>
    /// Получить общее количество транзакций в хранилище.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Количество транзакций в хранилище.</returns>
    public async Task<int> Count(CancellationToken cancellationToken = default) 
        => await storage.Transactions.Count(cancellationToken);

    /// <summary>
    /// Получить все транзакции с необходимыми фильтрами.
    /// </summary>
    /// <param name="wallet">Кошелёк, для которого нужно получить транзакции.</param>
    /// <param name="minCreatedAtUtc">Минимальная дата транзакции.</param>
    /// <param name="maxCreatedAtUtc">Максимальная дата транзакции.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список транзакций от старых к новым.</returns>
    public async Task<IReadOnlyList<Transaction>> Get(
        Wallet? wallet = null,
        DateTime? minCreatedAtUtc = null, 
        DateTime? maxCreatedAtUtc = null, 
        CancellationToken cancellationToken = default)
    {
        var transactions = await storage
            .Transactions
            .Get(
                wallet: wallet,
                minCreatedAtUtc: minCreatedAtUtc,
                maxCreatedAtUtc: maxCreatedAtUtc,
                cancellationToken: cancellationToken);

        return transactions;
    }

    /// <summary>
    /// Удалить все транзакции в хранилище.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task DeleteAll(CancellationToken cancellationToken = default)
        => await storage.Transactions.DeleteAll(cancellationToken);
}
