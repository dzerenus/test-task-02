using Bank.App.Interfaces;
using Bank.Cli.Interfaces;
using Bank.Cli.Models;
using Bank.Core.Enums;

namespace Bank.Cli.Commands;

internal abstract class BaseCommandTask : BaseCommand
{
    public BaseCommandTask(
        IConsole console,
        ILogger logger) 
        : base(
            console, 
            logger)
    {
    }

    protected DateFilter? GetMonthFilter()
    {
        Console.Clear();

        Console.WriteLine("[1] Январь");
        Console.WriteLine("[2] Февраль");
        Console.WriteLine("[3] Март");
        Console.WriteLine("[4] Апрель");
        Console.WriteLine("[5] Май");
        Console.WriteLine("[6] Июнь");
        Console.WriteLine("[7] Июль");
        Console.WriteLine("[8] Август");
        Console.WriteLine("[9] Сентябрь");
        Console.WriteLine("[0] Октябрь");
        Console.WriteLine("[-] Ноябрь");
        Console.WriteLine("[+] Декабрь");
        Console.WriteLine();
        Console.WriteLine("Для выхода нажмите любую другую кнопку...");

        var monthKey = Console.ReadKey("Выберите месяц:");

        Console.Clear();

        int? monthNumber = monthKey switch
        {
            ConsoleKey.D1 => 1,
            ConsoleKey.D2 => 2,
            ConsoleKey.D3 => 3,
            ConsoleKey.D4 => 4,
            ConsoleKey.D5 => 5,
            ConsoleKey.D6 => 6,
            ConsoleKey.D7 => 7,
            ConsoleKey.D8 => 8,
            ConsoleKey.D9 => 9,
            ConsoleKey.D0 => 10,
            ConsoleKey.OemMinus => 11,
            ConsoleKey.OemPlus => 12,
            _ => null,
        };

        if (monthNumber == null)
            return null;

        var nowDate = DateTime.Now.Date;
        var firstMonthDayDate = nowDate.AddDays(-(nowDate.Day - 1));

        var dateMonthDifference = monthNumber.Value - firstMonthDayDate.Month;

        var minDate = firstMonthDayDate.AddMonths(dateMonthDifference);
        var maxDate = minDate.AddMonths(1).AddSeconds(-1);

        return new DateFilter(
            startDate: minDate,
            endDate: maxDate);
    }

    protected string GetCurrencyString(Currency currency)
    {
        if (currency == Currency.USD) return "USD";
        if (currency == Currency.RUB) return "RUB";

        throw new InvalidOperationException("Unexpected Currency!");
    }
}
