using Bank.Core.Models;
using Bank.Core.Enums;

namespace Bank.Storage.Entities;

internal class EntityWallet : EntityBaseModel, IEntity<Wallet>
{
    public string Title { get; set; }

    public Currency Currency { get; set; }

    public decimal InitialBalance { get; set; }

    public EntityWallet(Wallet model) : base(model)
    {
        Title = model.Title;
        Currency = model.Currency;
        InitialBalance = model.InitialBalance;
    }

    public EntityWallet(
        Guid id,
        string title,
        Currency currency,
        decimal initialBalance,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        Title = title;
        Currency = currency;
        InitialBalance = initialBalance;
    }

    public new Wallet GetModel()
    {
        return new Wallet(
            id: Id,
            title: Title,
            currency: Currency,
            initialBalance: InitialBalance,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
