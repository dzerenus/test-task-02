using Bank.App.Interfaces;
using Bank.Core.Models;
using Bank.Core.Enums;
using Bank.Storage;

namespace Bank.App.Services;

internal class ServiceBalance(IServiceStorage storage) : IServiceBalance
{
    public async Task<decimal> GetBalance(Wallet wallet, CancellationToken cancellationToken = default)
    {
        var transactions = await storage
            .Transactions
            .Get(
                wallet: wallet,
                cancellationToken: cancellationToken);

        var balance = wallet.InitialBalance;

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
