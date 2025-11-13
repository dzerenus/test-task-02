using Bank.Core.Enums;
using Bank.App.Interfaces;
using Bank.Cli.Interfaces;
using Bank.Cli.Models;

namespace Bank.Cli.Commands;

/// <summary>
/// Команда отображения все транзакций за указанный месяц.
/// </summary>
internal class CommandTaskTransactions : BaseCommandTask
{
    // Скопировано из ТЗ:
    // Для указанного месяца сгруппировать все транзакции по типу (Income/Expense),
    // отсортировать группы по общей сумме (по убыванию),
    // в каждой группе отсортировать транзакции по дате (от самых старых к самым новым).

    private readonly IServiceTransaction _transactionService;
    private readonly IServiceExchange _exchangeService;
    private readonly IServiceWallet _walletService;

    public CommandTaskTransactions(
        IServiceTransaction transactionService,
        IServiceExchange exchangeService,
        IServiceWallet walletService,
        IConsole console,
        ILogger logger) 
        : base(
            console, 
            logger)
    {
        _transactionService = transactionService;
        _exchangeService = exchangeService;
        _walletService = walletService;
    }

    protected override async Task DoCommand(CancellationToken cancellationToken = default)
    {
        // Получаем от пользователя месяц.
        var dateFilter = GetMonthFilter();
        if (dateFilter is null) return;

        Logger.Inf("Ожидайте загрузку данных...");
        Logger.Inf($"Выбран диапазон дат {dateFilter.StartDate:dd.MM.yyyy} - {dateFilter.EndDate:dd.MM.yyyy}...");

        // Загружаем все транзакции за указанный месяц.

        var transactions = await _transactionService.Get(
            minCreatedAtUtc: dateFilter.StartDate.ToUniversalTime(),
            maxCreatedAtUtc: dateFilter.EndDate.ToUniversalTime(),
            cancellationToken: cancellationToken);

        Logger.Inf($"Найдено {transactions.Count} транзакций!");
        Logger.Inf($"Сортировка в процессе...");

        // Выделяем уникальные ID кошельков транзакций.

        var walletIds = transactions
            .Select(x => x.WalletId)
            .Distinct()
            .ToList();

        // Получаем курсы валют уникальных кошельков.

        var walletsIdCurrencies = await GetWalletCurrencies(
            walletIds: walletIds, 
            cancellationToken: cancellationToken);

        // Формируем расширенные данные о транзакциях с 
        // обоими курсами валют для удобного анализа.

        var extendedTransactions = new List<TransactionExtended>();

        foreach (var transaction in transactions)
        {
            var currency = walletsIdCurrencies[transaction.WalletId];
            var rub = _exchangeService.ToRub(currency, transaction.Amount);
            var usd = _exchangeService.ToUsd(currency, transaction.Amount);

            var currencies = new Dictionary<Currency, decimal> 
            { 
                [rub.Currency] = rub.Amount,
                [usd.Currency] = usd.Amount,
            };

            var extended = new TransactionExtended(
                transaction: transaction,
                currencies: currencies);

            extendedTransactions.Add(extended);
        }

        // Сортируем транзакции в зависимости от требований.

        var sortedTransactions = extendedTransactions
            .OrderByDescending(x => x.Type)
            .ThenByDescending(x => x.Currencies[Currency.RUB])
            .ThenBy(x => x.CreatedAtUtc)
            .ToList();

        Console.Clear();

        // Вывод транзакций на экран.

        foreach (var transaction in sortedTransactions)
        {
            WriteExtended(transaction);
            Console.WriteLine("---");
        }
    }

    /// <summary>
    /// Получить валюты кошельков по их ID.
    /// </summary>
    /// <param name="walletIds">Список ID кошельков.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Слоарь, в котором ID кошелька соотносится с типом валюты.</returns>
    private async Task<IReadOnlyDictionary<Guid, Currency>> GetWalletCurrencies(
        IReadOnlyList<Guid> walletIds,
        CancellationToken cancellationToken = default)
    {
        var walletIdCurrency = new Dictionary<Guid, Currency>();

        foreach (var walletId in walletIds)
        {
            var wallet = await _walletService.Get(walletId, cancellationToken);
            if (wallet is null) throw new NullReferenceException("Wallet not found!");

            walletIdCurrency[wallet.Id] = wallet.Currency;
        }

        return walletIdCurrency;
    }

    /// <summary>
    /// Вывести в консоль расширенные данные транзакции.
    /// </summary>
    /// <param name="extended"></param>
    private void WriteExtended(TransactionExtended extended)
    {
        var type = GetTypeString(extended.Type);
        var amountRub = extended.Currencies[Currency.RUB];
        var amountUsd = extended.Currencies[Currency.USD];

        Console.WriteLine($"{type} — {extended.CreatedAtUtc:dd.MM.yyyy HH:mm:ss}");
        Console.WriteLine($"{Currency.RUB} {amountRub:0.##}");
        Console.WriteLine($"{Currency.USD} {amountUsd:0.##}");
    }

    /// <summary>
    /// Сделать строку из типа транзакции.
    /// </summary>
    /// <param name="type">Тип транзакции.</param>
    /// <returns>Строка ПРИХОД или РАСХОД.</returns>
    private string GetTypeString(TransactionType type)
    {
        if (type == TransactionType.Income) return "ПРИХОД";
        if (type == TransactionType.Expense) return "РАСХОД";

        throw new InvalidOperationException("Unexpected Transaction Type!");
    }
}
