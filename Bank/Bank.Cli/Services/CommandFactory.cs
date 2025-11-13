using Microsoft.Extensions.DependencyInjection;
using Bank.Cli.Enums;
using Bank.Cli.Commands;
using Bank.Cli.Interfaces;

namespace Bank.Cli.Services;

/// <summary>
/// Сервис формирования команд в зависимости от запроса.
/// </summary>
/// <param name="serviceProvider">Проводник сервисов.</param>
internal class CommandFactory(IServiceProvider serviceProvider) : ICommandFactory
{
    /// <summary>
    /// Сформировать сервис команды в зависимости от переданной команды.
    /// </summary>
    /// <param name="command">Выбранная команда.</param>
    /// <returns>Сервис команды.</returns>
    public ICommand CreateCommand(Command command)
    {
        var services = serviceProvider.GetServices<ICommand>();

        if (command == Command.WalletsDelete)
            return services.OfType<CommandWalletsDelete>().Single();

        if (command == Command.WalletsGenerate)
            return services.OfType<CommandWalletsGenerate>().Single();

        if (command == Command.TransactionsDelete)
            return services.OfType<CommandTransactionsDelete>().Single();

        if (command == Command.TransactionsGenerate)
            return services.OfType<CommandTransactionGenerate>().Single();

        if (command == Command.TaskTransactions)
            return services.OfType<CommandTaskTransactions>().Single();

        if (command == Command.TaskWallets)
            return services.OfType<CommandTaskWallets>().Single();

        throw new InvalidOperationException("Unexpected command!");
    }
}
