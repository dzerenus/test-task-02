using Bank.App.Interfaces;
using Bank.Cli.Interfaces;

namespace Bank.Cli.Commands;

internal class CommandWalletsGenerate : BaseCommand
{
    private readonly int _walletCount = 5;
    private readonly IServiceRandom _randomService;

    public CommandWalletsGenerate(IServiceRandom randomService, IConsole console, ILogger logger) : base(console, logger)
    {
        _randomService = randomService;
    }

    protected override async Task DoCommand(CancellationToken cancellationToken = default)
    {
        Logger.Inf($"Начата генерация {_walletCount} кошельков...");

        await _randomService.GenerateWallets(_walletCount, cancellationToken);

        Logger.Inf("Кошельки успешно сгенерированы!");
    }
}
