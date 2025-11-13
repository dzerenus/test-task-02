namespace Bank.App.Interfaces;

public interface IServiceRandom
{
    public Task GenerateWallets(
        int count, 
        CancellationToken cancellationToken = default);

    public Task GenerateWalletsTransactions(
        int count, 
        DateTime minDate,
        DateTime maxDate, 
        CancellationToken cancellationToken = default);
}
