namespace Bank.App.Configuration;

/// <summary>
/// Конфигурация курсов валют.
/// </summary>
public interface IConfugurationCurrencies
{
    /// <summary>
    /// Количество рублей, которые можно получить за один доллар.
    /// </summary>
    public decimal CurrencyUsdToRub { get; }
}
