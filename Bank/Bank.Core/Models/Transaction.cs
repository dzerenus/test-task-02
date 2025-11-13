using Bank.Core.Enums;

namespace Bank.Core.Models;

/// <summary>
/// Транзакция (получение или трата денежных средств).
/// </summary>
public class Transaction : BaseModel
{
    /// <summary>
    /// ID кошелька, к которому относится транзакция.
    /// </summary>
    public Guid WalletId { get; }

    /// <summary>
    /// Тип транзакции (приход или расход средств).
    /// </summary>
    public TransactionType Type { get; }

    /// <summary>
    /// Сумма транзакции.
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Описание транзакции.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Конструктор создания НОВОЙ транзакции для кошелька.
    /// </summary>
    /// <param name="wallet">Кошелёк, относительно которого произошла транзакция.</param>
    /// <param name="type">Тип транзакции.</param>
    /// <param name="amount">Сумма транзакции.</param>
    public Transaction(
        Wallet wallet,
        TransactionType type,
        decimal amount)
    {
        WalletId = wallet.Id;
        Type = type;
        Amount = amount;
    }

    /// <summary>
    /// Копирование транзакции.
    /// </summary>
    /// <param name="model">Копируемая транзакция.</param>
    public Transaction(Transaction model) : base(model)
    {
        WalletId = model.WalletId;
        Type = model.Type;
        Amount = model.Amount;
        Description = model.Description;
    }

    /// <summary>
    /// Восстановление транзакции (например, из базы данных).
    /// </summary>
    /// <param name="id">ID транзакции.</param>
    /// <param name="walletId">ID кошелька.</param>
    /// <param name="type">Тип транзакции.</param>
    /// <param name="amount">Сумма транзакции.</param>
    /// <param name="description">Описание транзакции.</param>
    /// <param name="createdAtUtc">Дата создания транзакции.</param>
    /// <param name="updatedAtUtc">Дата последнего изменения транзакции.</param>
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
