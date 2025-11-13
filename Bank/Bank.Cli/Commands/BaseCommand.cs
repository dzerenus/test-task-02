using Bank.App.Interfaces;
using Bank.Cli.Interfaces;

namespace Bank.Cli.Commands;

internal abstract class BaseCommand : ICommand
{
    protected IConsole Console { get; }

    protected ILogger Logger { get; }

    public BaseCommand(
        IConsole console, 
        ILogger logger)
    {
        Console = console;
        Logger = logger;
    }

    public async Task Execute(CancellationToken cancellationToken = default)
    {
        Console.Clear();
        Logger.Inf("Начато выполнение операции...");

        await DoCommand(cancellationToken);

        Console.ReadKey("Выполнение команды завершено, для продолжения нажмите любую кнопку...");
    }

    protected abstract Task DoCommand(CancellationToken cancellationToken = default);
}
