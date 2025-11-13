using Bank.App.Interfaces;
using Bank.Cli.Interfaces;

namespace Bank.Cli.Commands;

internal class CommandWalletsDelete : BaseCommand
{
    private readonly IServiceWallet _wallets;

    public CommandWalletsDelete(
        IServiceWallet wallets,
        IConsole console,
        ILogger logger) 
        : base(
            console, 
            logger)
    {
        _wallets = wallets;
    }

    protected override async Task DoCommand(CancellationToken cancellationToken = default)
    {
        Logger.Inf("Начато удаление всех кошельков...");

        await _wallets.DeleteAll(cancellationToken);

        Logger.Inf("Удаление кошельков завершено!");
    }
}
