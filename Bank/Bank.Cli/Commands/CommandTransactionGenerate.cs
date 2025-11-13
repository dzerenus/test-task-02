using Bank.App.Interfaces;
using Bank.Cli.Interfaces;

namespace Bank.Cli.Commands;

internal class CommandTransactionGenerate : BaseCommand
{
    private readonly int _transactionCountPerWallet = 20;
    private readonly IServiceRandom _randomService;

    public CommandTransactionGenerate(
        IServiceRandom randomService,
        IConsole console,
        ILogger logger)
        : base(
            console, 
            logger)
    {
        _randomService = randomService;
    }

    protected override async Task DoCommand(CancellationToken cancellationToken = default)
    {
        Logger.Inf("Начата генерация транзакций...");

        var dt = DateTime.Now.Date;
        var minDate = dt.AddDays(1 - dt.Day).AddMonths(1 - dt.Month);
        var maxDate = minDate.AddYears(1).AddSeconds(-1);

        Logger.Inf(
            $"Для каждого кошелька будет создано {_transactionCountPerWallet} транзакций " +
            $"между {minDate:dd.MM.yyyy HH:mm:ss} и {maxDate:dd.MM.yyyy HH:mm:ss}...");

        await _randomService.GenerateWalletsTransactions(
            count: _transactionCountPerWallet,
            minDate: minDate,
            maxDate: maxDate,
            cancellationToken: cancellationToken);

        Logger.Inf("Транзакции успешно созданы!");
    }
}
