namespace Bank.Core.Enums;

/// <summary>
/// Тип транзакции.
/// </summary>
public enum TransactionType
{
    /// <summary> Расход. </summary>
    Expense = -1,

    /// <summary> Приход. </summary>
    Income = 1,
}
