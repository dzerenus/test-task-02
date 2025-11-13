namespace Bank.Cli.Interfaces;

/// <summary>
/// Сервис вывода и чтения данных из консоли.
/// </summary>
internal interface IConsole
{
    /// <summary>
    /// Вывести сообщение и получить нажатую пользователем клавишу.
    /// </summary>
    /// <param name="message">Текстовое сообщение.</param>
    /// <returns>Нажатая пользователем клавиша.</returns>
    public ConsoleKey ReadKey(string message);

    /// <summary>
    /// Вывести сообщение в консоль.
    /// </summary>
    /// <param name="message">Текстовое сообщение.</param>
    public void WriteLine(string message);

    /// <summary>
    /// Вывести в консоль пустую строку.
    /// </summary>
    public void WriteLine();
    
    /// <summary>
    /// Очистить данные в консоли.
    /// </summary>
    public void Clear();
}
