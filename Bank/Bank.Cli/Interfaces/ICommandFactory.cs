using Bank.Cli.Enums;

namespace Bank.Cli.Interfaces;

internal interface ICommandFactory
{
    public ICommand CreateCommand(Command command);
}
