namespace Bank.Cli.Models;

/// <summary>
/// Данные фильтрации по датам.
/// </summary>
/// <param name="startDate">Стартовая дата.</param>
/// <param name="endDate">Конечная дата.</param>
internal class DateFilter(DateTime startDate, DateTime endDate)
{
    /// <summary>
    /// Стартовая дата.
    /// </summary>
    public DateTime StartDate { get; } = startDate;

    /// <summary>
    /// Конечная дата.
    /// </summary>
    public DateTime EndDate { get; } = endDate;
}
