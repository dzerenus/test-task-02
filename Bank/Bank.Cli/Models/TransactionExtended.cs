using Bank.Core.Enums;
using Bank.Core.Models;

namespace Bank.Cli.Models;

internal class TransactionExtended(
    Transaction transaction, 
    IReadOnlyDictionary<Currency, decimal> currencies)
{
    public Guid Id { get; } = transaction.Id;

    public Guid WalletId { get; } = transaction.WalletId;

    public TransactionType Type { get; } = transaction.Type;

    public string? Description { get; init; } = transaction.Description;

    public IReadOnlyDictionary<Currency, decimal> Currencies { get; } = currencies;

    public DateTime CreatedAtUtc { get; } = transaction.CreatedAtUtc;

    public DateTime UpdatedAtUtc { get; } = transaction.UpdatedAtUtc;
}
