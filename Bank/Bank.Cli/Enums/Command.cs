namespace Bank.Cli.Enums;

/// <summary>
/// Команды, которые может выполнять программа.
/// </summary>
internal enum Command
{
    /// <summary>
    /// Удалить все кошельки.
    /// </summary>
    WalletsDelete,

    /// <summary>
    /// Сгенерировать случайные кошельки.
    /// </summary>
    WalletsGenerate,

    /// <summary>
    /// Удалить все транзакции.
    /// </summary>
    TransactionsDelete,

    /// <summary>
    /// Сгенерировать случайные транзакции.
    /// </summary>
    TransactionsGenerate,

    /// <summary>
    /// Отобразить крупнейшие транзакции за выбранный месяц.
    /// </summary>
    TaskTransactions,

    /// <summary>
    /// Отобразить крупнейшие расходы для всех кошельков за выбранный месяц.
    /// </summary>
    TaskWallets,
}
