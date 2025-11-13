using Bank.Core.Enums;
using Bank.App.Interfaces;
using Bank.Cli.Interfaces;
using Bank.Cli.Models;

namespace Bank.Cli.Commands;

internal class CommandTaskTransactions : BaseCommandTask
{
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
        var dateFilter = GetMonthFilter();
        if (dateFilter is null) return;

        Logger.Inf("Ожидайте загрузку данных...");
        Logger.Inf($"Выбран диапазон дат {dateFilter.StartDate:dd.MM.yyyy}-{dateFilter.EndDate:dd.MM.yyyy}...");

        var transactions = await _transactionService.Get(
            minCreatedAtUtc: dateFilter.StartDate.ToUniversalTime(),
            maxCreatedAtUtc: dateFilter.EndDate.ToUniversalTime(),
            cancellationToken: cancellationToken);

        Logger.Inf($"Найдено {transactions.Count} транзакций!");
        Logger.Inf($"Сортировка в процессе...");

        var walletIds = transactions
            .Select(x => x.WalletId)
            .Distinct()
            .ToList();

        var walletsIdCurrencies = await GetWalletCurrencies(
            walletIds: walletIds, 
            cancellationToken: cancellationToken);

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

        var sortedTransactions = extendedTransactions
            .OrderByDescending(x => x.Type)
            .ThenByDescending(x => x.Currencies[Currency.RUB])
            .ThenBy(x => x.CreatedAtUtc)
            .ToList();

        Console.Clear();

        foreach (var transaction in sortedTransactions)
        {
            WriteExtended(transaction);
            Console.WriteLine("---");
        }
    }

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

    private void WriteExtended(TransactionExtended extended)
    {
        var type = GetTypeString(extended.Type);
        var amountRub = extended.Currencies[Currency.RUB];
        var amountUsd = extended.Currencies[Currency.USD];

        Console.WriteLine($"{type} — {extended.CreatedAtUtc:dd.MM.yyyy HH:mm:ss}");
        Console.WriteLine($"{GetCurrencyString(Currency.RUB)} {amountRub:0.##}");
        Console.WriteLine($"{GetCurrencyString(Currency.USD)} {amountUsd:0.##}");
    }

    private string GetTypeString(TransactionType type)
    {
        if (type == TransactionType.Income) return "Приход";
        if (type == TransactionType.Expense) return "Расход";

        throw new InvalidOperationException("Unexpected Transaction Type!");
    }
}
