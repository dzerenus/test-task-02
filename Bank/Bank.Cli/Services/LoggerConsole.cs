using Bank.App.Interfaces;
using Bank.Cli.Interfaces;

namespace Bank.Cli.Services;

internal class LoggerConsole(IConsole consoleService) : ILogger
{
    public void Err(string message) => WriteWithPrefixAndTime("ERR", message);

    public void Wrn(string message) => WriteWithPrefixAndTime("WRN", message);

    public void Inf(string message) => WriteWithPrefixAndTime("INF", message);

    private void WriteWithPrefixAndTime(string prefix, string message)
        => consoleService.WriteLine($"{prefix} {CurrentTimeString()} {message}");

    private static string CurrentTimeString() => DateTime.Now.ToString("[HH:mm:ss]");

}
