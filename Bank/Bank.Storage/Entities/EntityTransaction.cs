using Bank.Core.Enums;
using Bank.Core.Models;

namespace Bank.Storage.Entities;

internal class EntityTransaction : EntityBaseModel, IEntity<Transaction>
{
    public Guid WalletId { get; set; }

    public TransactionType Type { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public EntityTransaction(Transaction model) : base(model)
    {
        WalletId = model.WalletId;
        Type = model.Type;
        Amount = model.Amount;
        Description = model.Description;
    }

    public EntityTransaction(
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
    
    public new Transaction GetModel()
    {
        return new Transaction(
            id: Id,
            walletId: WalletId,
            type: Type,
            amount: Amount,
            description: Description,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
