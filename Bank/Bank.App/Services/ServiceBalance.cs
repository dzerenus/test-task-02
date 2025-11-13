using Bank.Core.Models;
using Bank.Core.Enums;
using Bank.Storage;
using Bank.App.Interfaces;

namespace Bank.App.Services;

/// <summary>
/// Сервис управления балансом кошельков.
/// </summary>
/// <param name="storage">Сервис взаимодействия с хранилищем.</param>
internal class ServiceBalance(IServiceStorage storage) : IServiceBalance
{
    /// <summary>
    /// Получить текущий баланс кошелька.
    /// В текущий баланс входит стартовый баланс и сумма всех транзакций.
    /// </summary>
    /// <param name="wallet">Кошелёк, для которого будет получен баланс.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Текущий баланс переданного кошелька.</returns>
    public async Task<decimal> GetBalance(
        Wallet wallet, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(wallet);

        var balance = wallet.InitialBalance;

        // Получаем все транзакции кошелька из хранилища.
        var transactions = await storage
            .Transactions
            .Get(
                wallet: wallet,
                cancellationToken: cancellationToken);

        // Суммируем все транзакции в зависимости от их типа.
        foreach (var transaction in transactions)
        {
            switch (transaction.Type)
            {
                case TransactionType.Income:
                    balance += transaction.Amount;
                    break;

                case TransactionType.Expense:
                    balance -= transaction.Amount;
                    break;

                default:
                    throw new InvalidOperationException("Unexpected transaction type!");
            }
        }

        return balance; 
    }
}
