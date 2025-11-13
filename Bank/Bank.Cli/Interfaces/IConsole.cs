namespace Bank.Cli.Interfaces;

internal interface IConsole
{
    public ConsoleKey ReadKey(string message);

    public void WriteLine(string message);

    public void WriteLine();

    public void Clear();
}
