using Bank.App.Interfaces;
using Bank.Cli.Interfaces;

namespace Bank.Cli.Commands;

/// <summary>
/// Базовый класс команды, которую выполняет программа.
/// </summary>
internal abstract class BaseCommand : ICommand
{
    /// <summary>
    /// Сервис вывода в консоль.
    /// </summary>
    protected IConsole Console { get; }

    /// <summary>
    /// Сервис логгирования данных.
    /// </summary>
    protected ILogger Logger { get; }

    public BaseCommand(
        IConsole console, 
        ILogger logger)
    {
        Console = console;
        Logger = logger;
    }

    /// <summary>
    /// Начать выполнение команды и дождаться нажатия любой клавиши от пользователя после её завершения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task Execute(CancellationToken cancellationToken = default)
    {
        Console.Clear();
        Logger.Inf("Начато выполнение операции...");

        await DoCommand(cancellationToken);

        Console.ReadKey("Выполнение команды завершено, для продолжения нажмите любую кнопку...");
    }

    /// <summary>
    /// Непосредственное выполнение команды.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    protected abstract Task DoCommand(CancellationToken cancellationToken = default);
}
