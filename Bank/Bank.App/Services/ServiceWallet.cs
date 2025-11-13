using Bank.Core.Models;
using Bank.Storage;
using Bank.App.Interfaces;

namespace Bank.App.Services;

/// <summary>
/// Сервис управления кошельками.
/// </summary>
/// <param name="storage">Сервис взаимодействия с хранилищем.</param>
internal class ServiceWallet(IServiceStorage storage) : IServiceWallet
{
    /// <summary>
    /// Получить количество кошельков в хранилище.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Общее количество кошельков.</returns>
    public async Task<int> Count(CancellationToken cancellationToken = default) 
        => await storage.Wallets.Count(cancellationToken);

    /// <summary>
    /// Получить все кошельки из хранилища.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Все кошельки от старых к новым.</returns>
    public async Task<IReadOnlyList<Wallet>> Get(CancellationToken cancellationToken = default)
        => await storage.Wallets.Get(cancellationToken);
    
    /// <summary>
    /// Получить кошелёк по его ID.
    /// </summary>
    /// <param name="id">ID кошелька.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Кошелёк, а если он не существует - NULL.</returns>
    public async Task<Wallet?> Get(
        Guid id, 
        CancellationToken cancellationToken = default)
        => await storage.Wallets.Get(id, cancellationToken);

    /// <summary>
    /// Удалить все кошельки из хранилища.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task DeleteAll(CancellationToken cancellationToken = default)
        => await storage.Wallets.DeleteAll(cancellationToken);
}
