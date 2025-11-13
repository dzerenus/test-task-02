using Bank.Core.Enums;

namespace Bank.Core.Models;

public class Transaction : BaseModel
{
    public Guid WalletId { get; }

    public TransactionType Type { get; }

    public decimal Amount { get; }

    public string? Description { get; init; }

    public Transaction(
        Wallet wallet,
        TransactionType type,
        decimal amount)
    {
        WalletId = wallet.Id;
        Type = type;
        Amount = amount;
    }

    public Transaction(Transaction model) : base(model)
    {
        WalletId = model.WalletId;
        Type = model.Type;
        Amount = model.Amount;
        Description = model.Description;
    }

    public Transaction(
        Guid id,
        Guid walletId,
        TransactionType type,
        decimal amount,
        string? description,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        WalletId = walletId;
        Type = type;
        Amount = amount;
        Description = description;
    }
}
