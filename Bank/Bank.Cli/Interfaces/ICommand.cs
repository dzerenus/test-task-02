namespace Bank.Cli.Interfaces;

public interface ICommand
{
    public Task Execute(CancellationToken cancellationToken = default);
}
