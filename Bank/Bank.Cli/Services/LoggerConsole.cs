using Bank.App.Interfaces;
using Bank.Cli.Interfaces;

namespace Bank.Cli.Services;

/// <summary>
/// Сервис логирования данных в консоль.
/// </summary>
/// <param name="consoleService">Сервис вывода текста в консоль.</param>
internal class LoggerConsole(IConsole consoleService) : ILogger
{
    /// <summary>
    /// Вывести в консоль сообщение об ошибке.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    public void Err(string message) => WriteWithPrefixAndTime("ERR", message);

    /// <summary>
    /// Вывести в консоль предупреждающее сообщение.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    public void Wrn(string message) => WriteWithPrefixAndTime("WRN", message);

    /// <summary>
    /// Вывести в консоль информационное сообщение.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    public void Inf(string message) => WriteWithPrefixAndTime("INF", message);

    /// <summary>
    /// Вывести в консоль сообщение с указанным префиксом и текущем временем.
    /// </summary>
    /// <param name="prefix">Префикс сообщения.</param>
    /// <param name="message">Текст сообщения.</param>
    private void WriteWithPrefixAndTime(string prefix, string message)
        => consoleService.WriteLine($"{prefix} {DateTime.Now:[HH:mm:ss]} {message}");
}
