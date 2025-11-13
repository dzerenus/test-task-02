using Bank.Cli.Interfaces;

namespace Bank.Cli.Services;

internal class ServiceConsole : IConsole
{
    public void WriteLine(string message) => Console.WriteLine(message);

    public void WriteLine() => Console.WriteLine();

    public void Clear() => Console.Clear();

    public ConsoleKey ReadKey(string message)
    {
        Console.Write(message);
        var key = Console.ReadKey();
        Console.WriteLine();
        return key.Key;
    }
}
