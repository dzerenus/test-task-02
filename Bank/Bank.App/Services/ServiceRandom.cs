using Bank.App.Configuration;
using Bank.App.Interfaces;
using Bank.Core.Enums;
using Bank.Core.Models;
using Bank.Storage;

namespace Bank.App.Services;

internal class ServiceRandom(
    IServiceStorage storage, 
    IServiceBalance balanceService,
    IConfigurationTransaction configurationTransaction) 
    : IServiceRandom
{
    public async Task GenerateWallets(int count, CancellationToken cancellationToken = default)
    {
        var wallets = new List<Wallet>();

        while (wallets.Count < count)
        {
            var wallet = CreateRandomWallet();
            wallets.Add(wallet);
        }

        await storage.Wallets.Insert(wallets, cancellationToken);
    }

    public async Task GenerateWalletsTransactions(
        int count, 
        DateTime minDate, 
        DateTime maxDate, 
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        var totalDays = (int)(maxDate - minDate).TotalDays;
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(totalDays);

        var wallets = await storage.Wallets.Get(cancellationToken);
        var transactions = new List<Transaction>();

        foreach (var wallet in wallets)
        {
            var walletTransactions = await GenerateWalletTransactions(
                wallet: wallet,
                transactionCount: count,
                minDate: minDate,
                daysCount: totalDays,
                cancellationToken: cancellationToken);

            transactions.AddRange(walletTransactions);
        }

        await storage
            .Transactions
            .Insert(transactions, cancellationToken);
    }

    private async Task<IReadOnlyList<Transaction>> GenerateWalletTransactions(
        Wallet wallet,
        int transactionCount,
        DateTime minDate,
        int daysCount,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(wallet);
        ArgumentOutOfRangeException.ThrowIfNegative(transactionCount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(daysCount);

        var transactions = new List<Transaction>();
        var dayTransactions = new int[daysCount];
        var balance = await balanceService.GetBalance(wallet, cancellationToken);

        for (int transactionIndex = 0; transactionIndex < transactionCount; transactionIndex++)
        {
            var dayIndex = Random.Shared.Next(0, dayTransactions.Length);
            dayTransactions[dayIndex]++;
        }

        for (int dayIndex = 0; dayIndex < daysCount; dayIndex++)
        {
            var dayTransactionCount = dayTransactions[dayIndex];
            if (dayTransactionCount == 0) continue;

            var hoursTransactions = new int[24];

            for (int dayTransactionIndex = 0; dayTransactionIndex < dayTransactionCount; dayTransactionIndex++)
            {
                var hourIndex = Random.Shared.Next(hoursTransactions.Length);
                hoursTransactions[hourIndex]++;
            }

            for (int hourIndex = 0; hourIndex < hoursTransactions.Length; hourIndex++)
            {
                var hourTransactions = hoursTransactions[hourIndex];

                for (int minuteIndex = 0; minuteIndex < hourTransactions; minuteIndex++)
                {
                    var dateUtc = minDate
                        .AddDays(dayIndex)
                        .AddHours(hourIndex)
                        .AddMinutes(minuteIndex)
                        .ToUniversalTime();

                    var minOperationAmount = wallet.Currency == Currency.USD
                        ? configurationTransaction.MinTransactionAmountUsd
                        : configurationTransaction.MinTransactionAmountRub;

                    var maxOperationAmount = wallet.Currency == Currency.USD
                        ? configurationTransaction.MaxTransactionAmountUsd
                        : configurationTransaction.MaxTransactionAmountRub;

                    var type = balance > minOperationAmount && RandomBool(0.45)
                        ? TransactionType.Expense
                        : TransactionType.Income;

                    var maxExpenseAmount = balance > maxOperationAmount
                        ? maxOperationAmount
                        : (int)balance;

                    var amount = type == TransactionType.Income
                        ? (decimal)Random.Shared.Next(minOperationAmount * 100, maxOperationAmount * 100) / 100
                        : (decimal)Random.Shared.Next(minOperationAmount * 100, maxExpenseAmount * 100) / 100;

                    var description = RandomBool(0.3)
                        ? RandomString("Описание")
                        : null;

                    var transaction = new Transaction(
                        id: Guid.NewGuid(),
                        walletId: wallet.Id,
                        type: type,
                        amount: amount,
                        description: description,
                        createdAtUtc: dateUtc,
                        updatedAtUtc: dateUtc);

                    balance += (amount * (type == TransactionType.Income ? 1 : -1));
                    transactions.Add(transaction);
                }
            }
        }

        return transactions;
    }

    private static Wallet CreateRandomWallet()
    {
        var title = RandomString("Кошелёк");

        var currency = RandomBool()
            ? Currency.RUB
            : Currency.USD;

        var initialBalance = (decimal)Random.Shared.Next(10_000) / 100;

        var wallet = new Wallet(
            title: title,
            currency: currency,
            initialBalance: initialBalance);

        return wallet;
    }


    private static string RandomString(
        string? prefix = null,
        string? splitter = "_",
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789",
        int randomPartLength = 8)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(randomPartLength);

        var result = prefix is null
            ? string.Empty
            : prefix;

        if (splitter is not null)
            result += splitter;

        result += new string(
            Enumerable
                .Repeat(alphabet, randomPartLength)
                .Select(letter => letter[Random.Shared.Next(letter.Length)])
                .ToArray());

        return result;
    }

    private static bool RandomBool(double chance = 0.5)
        => Random.Shared.NextDouble() < chance;
}
