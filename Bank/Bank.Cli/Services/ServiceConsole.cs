using Bank.Cli.Interfaces;

namespace Bank.Cli.Services;

/// <summary>
/// Сервис вывода сообщений в консоль.
/// </summary>
internal class ServiceConsole : IConsole
{
    /// <summary>
    /// Вывести в консоль сообщение.
    /// </summary>
    /// <param name="message">Текстовое сообщение.</param>
    public void WriteLine(string message) => Console.WriteLine(message);

    /// <summary>
    /// Вывести в консоль пустую строку.
    /// </summary>
    public void WriteLine() => Console.WriteLine();

    /// <summary>
    /// Очистить консоль.
    /// </summary>
    public void Clear() => Console.Clear();

    /// <summary>
    /// Вывести сообщение и считать нажатие пользователя.
    /// </summary>
    /// <param name="message">Текстовое сообщение.</param>
    /// <returns>Нажатая пользователем клавиша.</returns>
    public ConsoleKey ReadKey(string message)
    {
        Console.Write(message);
        var key = Console.ReadKey();
        Console.WriteLine();
        return key.Key;
    }
}
