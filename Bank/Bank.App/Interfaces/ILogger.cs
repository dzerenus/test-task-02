namespace Bank.App.Interfaces;

/// <summary>
/// Сервис логирования данных.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Записать информационное сообщение.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    public void Inf(string message);

    /// <summary>
    /// Записать сообщение, требующее внимания.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    public void Wrn(string message);

    /// <summary>
    /// Записать сообщение об ошибке.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    public void Err(string message);
}
