namespace Bank.Cli.Interfaces;

/// <summary>
/// Сервис выполняемой команды.
/// Каждая отдельная команда, подаваемая пользователем в консоли,
/// является отдельным сервисом, который её выполняет.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Выполнить команду пользователя.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task Execute(CancellationToken cancellationToken = default);
}
