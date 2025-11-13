using Bank.Core.Models;
using Bank.Core.Enums;
using Bank.Storage;
using Bank.App.Configuration;
using Bank.App.Interfaces;

namespace Bank.App.Services;

/// <summary>
/// Сервис генерации случайных данных.
/// </summary>
/// <param name="storage">Сервис управления хранилищем.</param>
/// <param name="balanceService">Сервис управления балансом.</param>
/// <param name="configurationTransaction">Конфигурация размеров транзакций.</param>
internal class ServiceRandom(
    IServiceStorage storage, 
    IServiceBalance balanceService,
    IConfigurationTransaction configurationTransaction) 
    : IServiceRandom
{
    /// <summary>
    /// Генерация случайных кошельков.
    /// </summary>
    /// <param name="count">Количество генерируемых кошельков.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task GenerateWallets(
        int count, 
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        // Сперва создаём все кошельки.
        // А потом разом загружаем их в хранилище..

        var wallets = new List<Wallet>();

        while (wallets.Count < count)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var wallet = CreateRandomWallet();
            wallets.Add(wallet);
        }

        await storage
            .Wallets
            .Insert(
                wallets: wallets,
                cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Создать случайные транзакции.
    /// </summary>
    /// <param name="count">Количество транзакций для каждого кошелька.</param>
    /// <param name="minDate">Минимальная дата транзакции.</param>
    /// <param name="maxDate">Максимальная дата транзакции.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task GenerateWalletsTransactions(
        int count, 
        DateTime minDate, 
        DateTime maxDate, 
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        var totalDays = (int)(maxDate - minDate).TotalDays;
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(totalDays);

        var wallets = await storage
            .Wallets
            .Get(cancellationToken);

        // Сперва создаём все транзакции,
        // а потом заталкиваем их в базу данных одной пачкой.

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
            .Insert(
                transactions: transactions,
                cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Сгенерировать транзакции для отдельного кошелька.
    /// </summary>
    /// <param name="wallet">Кошелёк.</param>
    /// <param name="transactionCount">Количество транзакций.</param>
    /// <param name="minDate">Минимальная дата транзакции.</param>
    /// <param name="daysCount">Количество дней от минимальной даты, до которой можно генерировать.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список сгенерированных транзакций.</returns>
    private async Task<IReadOnlyList<Transaction>> GenerateWalletTransactions(
        Wallet wallet,
        int transactionCount,
        DateTime minDate,
        int daysCount,
        CancellationToken cancellationToken)
    {
        var transactions = new List<Transaction>();

        // Этап 1: Распределяем генерируемые транзакции по всему диапазону дней.

        var dayTransactions = new int[daysCount];

        for (int transactionIndex = 0; transactionIndex < transactionCount; transactionIndex++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dayIndex = Random
                .Shared
                .Next(dayTransactions.Length);

            dayTransactions[dayIndex]++;
        }

        // Этап 2: Получаем актуальный баланс кошелька.

        var balance = await balanceService
            .GetBalance(
                wallet: wallet,
                cancellationToken: cancellationToken);

        // Этап 3: Генерация транзакций для каждого отдельного дня.

        for (int dayIndex = 0; dayIndex < daysCount; dayIndex++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dayTransactionCount = dayTransactions[dayIndex];
            if (dayTransactionCount == 0) continue;

            // Этап 3.1: Равномерно распределяем транзакции по часам.
            // Важно, чтобы в каждый час было не более 60 транзакций.

            var hoursTransactions = new int[24];

            for (int dayTransactionIndex = 0; dayTransactionIndex < dayTransactionCount; dayTransactionIndex++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var hourIndex = Random
                    .Shared
                    .Next(hoursTransactions.Length);

                if (hoursTransactions[hourIndex] < 60)
                    hoursTransactions[hourIndex]++;
            }

            // Этап 3.2: Непосредственно генерируем транзакции для каждого часа дня,
            // в которые они были распределены.

            for (int hourIndex = 0; hourIndex < hoursTransactions.Length; hourIndex++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var hourTransactions = hoursTransactions[hourIndex];

                // Если в один час отмечено несколько транзакций,
                // генерируем их со смещением в одну минуту.

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

    /// <summary>
    /// Создать случайный кошелёк.
    /// </summary>
    /// <returns>Созданный кошелёк.</returns>
    private Wallet CreateRandomWallet()
    {
        var title = RandomString("Кошелёк");

        var currency = RandomBool()
            ? Currency.RUB
            : Currency.USD;

        var minOperationAmount = currency == Currency.USD
            ? configurationTransaction.MinTransactionAmountUsd
            : configurationTransaction.MinTransactionAmountRub;

        var maxOperationAmount = currency == Currency.USD
            ? configurationTransaction.MaxTransactionAmountUsd
            : configurationTransaction.MaxTransactionAmountRub;

        var initialBalance = (decimal)Random.Shared.Next(minOperationAmount, maxOperationAmount) / 100;

        var wallet = new Wallet(
            title: title,
            currency: currency,
            initialBalance: initialBalance);

        return wallet;
    }

    /// <summary>
    /// Сгенерировать случайную строку.
    /// </summary>
    /// <param name="prefix">Префикс строки.</param>
    /// <param name="splitter">Разделитель префика и сгенерированной строки.</param>
    /// <param name="alphabet">Алфавит символов.</param>
    /// <param name="randomPartLength">Длина случайной части строки.</param>
    /// <returns>Созданная строка.</returns>
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

    /// <summary>
    /// Получить случайное значение Bool.
    /// </summary>
    /// <param name="chance">Вероятность получения True от 0 до 1</param>
    /// <returns>Случайный Bool с учётом вероятности.</returns>
    private static bool RandomBool(double chance = 0.5)
        => Random.Shared.NextDouble() < chance;
}
