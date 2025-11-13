using Microsoft.Extensions.Hosting;
using Bank.App.Interfaces;
using Bank.Cli.Enums;
using Bank.Cli.Interfaces;

namespace Bank.Cli.Services.Background;

/// <summary>
/// Главный сервис приложения.
/// </summary>
/// <param name="appLifetime">Сервис управления жизненным циклом приложения.</param>
/// <param name="commandFactory">Сервис формирования команд.</param>
/// <param name="transactionService">Сервис упрвления транзакциями.</param>
/// <param name="walletService">Сервис управления кошельками.</param>
/// <param name="console">Сервис вывода данных в консоль.</param>
/// <param name="logger">Сервис логгировани данных.</param>
internal class BackgroundServiceProgram(
    IHostApplicationLifetime appLifetime,
    ICommandFactory commandFactory,
    IServiceTransaction transactionService,
    IServiceWallet walletService,
    IConsole console,
    ILogger logger) 
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // 1. Показываем пользователю главное меню.
            // 2. Ожидаем от пользователя нажатия на кнопку.
            // 3. Если пользователь нажал кнопку выхода - выходим из цикла.
            // 4. Если пользователь нажал кнопку команды - формируем и выполнем команду.
            // 5. Если пользователь нажал другую кнопку - повторно показываем ему главное меню.

            var key = await AwaitCommand(stoppingToken);
            if (key == ConsoleKey.D9) break;

            Command? keyCommand = key switch
            {
                ConsoleKey.D1 => Command.WalletsGenerate,
                ConsoleKey.D2 => Command.TransactionsGenerate,
                ConsoleKey.D3 => Command.WalletsDelete,
                ConsoleKey.D4 => Command.TransactionsDelete,
                ConsoleKey.D5 => Command.TaskTransactions,
                ConsoleKey.D6 => Command.TaskWallets,
                _ => null
            };

            if (keyCommand == null) continue;

            var command = commandFactory.CreateCommand(keyCommand.Value);
            await command.Execute(stoppingToken);
        }

        appLifetime.StopApplication();
    }

    /// <summary>
    /// Получить от пользователя код операции, которую он хочет выполнить.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Нажатая пользователм клавиша.</returns>
    private async Task<ConsoleKey> AwaitCommand(CancellationToken cancellationToken = default)
    {
        console.Clear();
        logger.Inf("Загрузка информационных данных из базы данных...");

        var walletCount = await walletService.Count(cancellationToken);
        var transactionCount = await transactionService.Count(cancellationToken);
        
        console.Clear();

        console.WriteLine($"Всего кошельков: {walletCount}");
        console.WriteLine($"Всего транзакий: {transactionCount}");
        console.WriteLine();
        console.WriteLine($"[1]: Сгенерировать кошельки");
        console.WriteLine($"[2]: Сгенерировать транзакции");
        console.WriteLine($"[3]: Удалить все кошельки");
        console.WriteLine($"[4]: Удалить все транзакции");
        console.WriteLine($"[5]: Для указанного месяца сгруппировать все транзакции по типу -> сумме -> дате");
        console.WriteLine($"[6]: 3 самые большие траты за указанный месяц для каждого кошелька, отсортированные по убыванию суммы");
        console.WriteLine($"[9]: Завершить работу программы");

        return console.ReadKey("Введите команду:");
    }
}
