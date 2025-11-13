using Bank.App.Interfaces;
using Bank.Cli.Interfaces;

namespace Bank.Cli.Commands;

internal class CommandTransactionsDelete: BaseCommand
{
    private readonly IServiceTransaction _transactions;

    public CommandTransactionsDelete(
        IServiceTransaction transactions,
        IConsole console,
        ILogger logger) 
        : base(
            console, 
            logger)
    {
        _transactions = transactions;
    }

    protected override async Task DoCommand(CancellationToken cancellationToken = default)
    {
        Logger.Inf("Начато удаление всех транзакций...");

        await _transactions.DeleteAll(cancellationToken);

        Logger.Inf("Удаление транзакций завершено!");
    }
}