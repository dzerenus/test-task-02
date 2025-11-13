using Bank.Core.Enums;

namespace Bank.Core.Models;

public class Wallet : BaseModel
{
    public string Title { get; }

    public Currency Currency { get; }

    public decimal InitialBalance { get; }

    public Wallet(
        string title,
        Currency currency,
        decimal initialBalance)
    {
        Title = title;
        InitialBalance = initialBalance;
        Currency = currency;
    }

    public Wallet(Wallet model) : base(model)
    {
        Title = model.Title;
        Currency = model.Currency;
        InitialBalance = model.InitialBalance;
    }

    public Wallet(
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
}
