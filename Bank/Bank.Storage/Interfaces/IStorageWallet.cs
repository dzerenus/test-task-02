using Bank.Core.Models;

namespace Bank.Storage.Interfaces;

/// <summary>
/// Сервис управления хранилищем кошельков.
/// </summary>
public interface IStorageWallet
{
    /// <summary>
    /// Количество кошельков в базе данных.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Общее количество кошельков в базе данных.</returns>
    public Task<int> Count(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить конкретный кошелёк.
    /// </summary>
    /// <param name="id">ID кошелька.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Кошелёк, если он бал найден.</returns>
    public Task<Wallet?> Get(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить список всех кошельков.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список всех кошельков от старых к новым.</returns>
    public Task<IReadOnlyList<Wallet>> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить несколько новых кошельков в хранилище.
    /// </summary>
    /// <param name="wallets">Добавляемые кошельки.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task Insert(IReadOnlyList<Wallet> wallets, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить все кошельки из базы данных.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task DeleteAll(CancellationToken cancellationToken = default);
}
