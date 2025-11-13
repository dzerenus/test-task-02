using Bank.App.Interfaces;
using Bank.Cli.Interfaces;
using Bank.Core.Enums;
using Bank.Core.Models;

namespace Bank.Cli.Commands;

/// <summary>
/// Команда вывода крупнейших расходов кошелька за выбранный сервис.
/// </summary>
internal class CommandTaskWallets : BaseCommandTask
{
    // Скопировано из ТЗ:
    // 3 самые большие траты за указанный месяц для каждого кошелька,
    // отсортированные по убыванию суммы.

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
        // Пользователь выбирает месяц.

        var dateFilter = GetMonthFilter();
        if (dateFilter is null) return;

        Logger.Inf("Загрузка кошельков....");

        var wallets = await _walletService.Get(cancellationToken);

        Logger.Inf($"Загружено {wallets.Count} кошельков!");

        // Загружаем все транзакции для кошелька за выбранный месяц.

        Logger.Inf($"Загрузка транзакций...");

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

        // Отбираем необходимые транзакции и сортируем их в соответствии с условиями.

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

        // Вывод транзакций на экран.

        foreach (var walletTransactions in walletsTransactions)
        {
            var wallet = walletTransactions.Key;
            var transactions = walletTransactions.Value;

            Console.WriteLine($"{wallet.Currency} {wallet.Title}");

            foreach (var transaction in transactions)
            {
                var transactionString = $"> РАСХОД: {transaction.Amount:0.##}";

                if (transaction.Description is not null)
                    transactionString += $" — {transaction.Description}";

                Console.WriteLine(transactionString);
            }

            Console.WriteLine($"---");
        }
    }
}
