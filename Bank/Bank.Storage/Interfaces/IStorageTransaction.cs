using Bank.Core.Models;

namespace Bank.Storage.Interfaces;

/// <summary>
/// Сервис управления хранилищем транзакций.
/// </summary>
public interface IStorageTransaction
{
    /// <summary>
    /// Получить количество транзакций в хранилище.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Общее количество транзакций в хранилище</returns>
    public Task<int> Count(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить транзакции с необходимыми фильтрами.
    /// </summary>
    /// <param name="wallet">Кошелёк, к которому должны относится транзакции.</param>
    /// <param name="minCreatedAtUtc">Миниммальная дата транзакции по UTC.</param>
    /// <param name="maxCreatedAtUtc">Максимальная дата транзакции по UTC.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список транзакций от старых к новым.</returns>
    public Task<IReadOnlyList<Transaction>> Get(
        Wallet? wallet = null,
        DateTime? minCreatedAtUtc = null,
        DateTime? maxCreatedAtUtc = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить несколько новых транзакций в хранилище.
    /// </summary>
    /// <param name="transactions">Список транзакций.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task Insert(
        IReadOnlyList<Transaction> transactions, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить все транзакции из хранилища.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task DeleteAll(CancellationToken cancellationToken = default);
}
