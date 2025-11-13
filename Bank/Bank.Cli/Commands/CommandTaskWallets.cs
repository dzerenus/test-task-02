using Bank.App.Interfaces;
using Bank.Cli.Interfaces;
using Bank.Core.Enums;
using Bank.Core.Models;

namespace Bank.Cli.Commands;

internal class CommandTaskWallets : BaseCommandTask
{
    private readonly IServiceWallet _walletService;
    private readonly IServiceTransaction _transactionService;

    public CommandTaskWallets(
        IServiceTransaction transactionService,
        IServiceWallet walletService,
        IConsole console, 
        ILogger logger) 
        : base(
            console, 
            logger)
    {
        _transactionService = transactionService;
        _walletService = walletService;
    }

    protected override async Task DoCommand(CancellationToken cancellationToken = default)
    {
        var dateFilter = GetMonthFilter();
        if (dateFilter is null) return;

        Logger.Inf("Загрузка кошельков....");

        var wallets = await _walletService.Get(cancellationToken);

        Logger.Inf($"Загружено {wallets.Count} кошельков!");
        Logger.Inf($"Загружено транзакций...");

        var walletsTransactions = new Dictionary<Wallet, IReadOnlyList<Transaction>>();
        
        foreach (var wallet in wallets)
        {
            var transactions = await _transactionService.Get(
                wallet: wallet,
                minCreatedAtUtc: dateFilter.StartDate.ToUniversalTime(),
                maxCreatedAtUtc: dateFilter.EndDate.ToUniversalTime(),
                cancellationToken: cancellationToken);

            Logger.Inf($"Для кошелька «{wallet.Title}» загружено транзакций: {transactions.Count}");
            walletsTransactions[wallet] = transactions;
        }

        Logger.Inf($"Выполняется сортировка...");

        var walletsTransactionsPairs = walletsTransactions.ToList();

        for (int walletTransactionsIndex = 0; walletTransactionsIndex < walletsTransactionsPairs.Count; walletTransactionsIndex++)
        {
            var walletTransactions = walletsTransactionsPairs[walletTransactionsIndex];

            var wallet = walletTransactions.Key;
            var transactions = walletTransactions.Value;

            Logger.Inf($"Обработка {walletTransactionsIndex + 1}/{walletsTransactionsPairs.Count}...");

            walletsTransactions[wallet] = transactions
                .Where(x => x.Type == TransactionType.Expense)
                .OrderByDescending(x => x.Amount)
                .Take(3)
                .ToList();
        }

        Logger.Inf($"Обработка завершена!");
        Console.Clear();

        foreach (var walletTransactions in walletsTransactions)
        {
            var wallet = walletTransactions.Key;
            var transactions = walletTransactions.Value;

            var currency = GetCurrencyString(wallet.Currency);

            Console.WriteLine($"{currency} {wallet.Title}");

            foreach (var transaction in transactions)
                Console.WriteLine($"> РАСХОД: {transaction.Amount:0.##}");

            Console.WriteLine($"---");
        }
    }
}
