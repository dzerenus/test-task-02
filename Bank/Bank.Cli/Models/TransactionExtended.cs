using Bank.Core.Enums;
using Bank.Core.Models;

namespace Bank.Cli.Models;

/// <summary>
/// Расширенные данные о транзакции.
/// </summary>
/// <param name="transaction">Транзакция.</param>
/// <param name="currencies">Суммы в разных валютах.</param>
internal class TransactionExtended(
    Transaction transaction, 
    IReadOnlyDictionary<Currency, decimal> currencies)
{
    /// <summary>
    /// ID транзакции.
    /// </summary>
    public Guid Id { get; } = transaction.Id;

    /// <summary>
    /// ID кошелька, к которому относится транзакция.
    /// </summary>
    public Guid WalletId { get; } = transaction.WalletId;

    /// <summary>
    /// Тип транзакции (приход/расход).
    /// </summary>
    public TransactionType Type { get; } = transaction.Type;

    /// <summary>
    /// Описание транзакции.
    /// </summary>
    public string? Description { get; init; } = transaction.Description;

    /// <summary>
    /// Сумма транзакции в разных валютах.
    /// </summary>
    public IReadOnlyDictionary<Currency, decimal> Currencies { get; } = currencies;

    /// <summary>
    /// Дата создания транзакции.
    /// </summary>
    public DateTime CreatedAtUtc { get; } = transaction.CreatedAtUtc;

    /// <summary>
    /// Дата обновления транзакции.
    /// </summary>
    public DateTime UpdatedAtUtc { get; } = transaction.UpdatedAtUtc;
}
