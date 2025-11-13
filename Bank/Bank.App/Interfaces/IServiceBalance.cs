using Bank.Core.Models;

namespace Bank.App.Interfaces;

/// <summary>
/// Сервис управления балансом.
/// </summary>
public interface IServiceBalance
{
    /// <summary>
    /// Получить текущий баланс кошелька (стартовый баланс + сумма всех транзакций).
    /// </summary>
    /// <param name="wallet">Кошелёк, для которого нужно подсчитать баланс.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Текущий баланс кошелька.</returns>
    public Task<decimal> GetBalance(
        Wallet wallet, 
        CancellationToken cancellationToken = default);
}
