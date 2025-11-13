using Bank.Cli.Enums;

namespace Bank.Cli.Interfaces;

/// <summary>
/// Сервис формирования сервисов выполнения команд в зависимости от выбранной команды.
/// </summary>
internal interface ICommandFactory
{
    /// <summary>
    /// Сформировать сервис выполнения команды.
    /// </summary>
    /// <param name="command">Выполняемая команда.</param>
    /// <returns>Сервис выполняемой команды.</returns>
    public ICommand CreateCommand(Command command);
}
