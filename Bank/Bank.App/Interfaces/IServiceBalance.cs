using Bank.Core.Models;

namespace Bank.App.Interfaces;

public interface IServiceBalance
{
    public Task<decimal> GetBalance(Wallet wallet, CancellationToken cancellationToken = default);
}
