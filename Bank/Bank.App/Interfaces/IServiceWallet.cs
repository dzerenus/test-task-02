using Bank.Core.Models;

namespace Bank.App.Interfaces;

/// <summary>
/// Сервис управления кошельками.
/// </summary>
public interface IServiceWallet
{
    /// <summary>
    /// Получить количество кошельков в хранилище.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Общее количество кошельков в хранилище.</returns>
    public Task<int> Count(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить все кошельки в хранилище.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список кошельков от старых к новым.</returns>
    public Task<IReadOnlyList<Wallet>> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить конкретный кошелёк по его ID.
    /// </summary>
    /// <param name="id">ID кошелька.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Кошелёк, если он существует, иначе NULL.</returns>
    public Task<Wallet?> Get(
        Guid id, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить все кошельки из хранилища.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task DeleteAll(CancellationToken cancellationToken = default);
}
