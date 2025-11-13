namespace Bank.Cli.Models;

internal class DateFilter(DateTime startDate, DateTime endDate)
{
    public DateTime StartDate { get; } = startDate;

    public DateTime EndDate { get; } = endDate;
}
