using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Bank.App;
using Bank.App.Interfaces;
using Bank.Cli;
using Bank.Cli.Commands;
using Bank.Cli.Interfaces;
using Bank.Cli.Services;
using Bank.Cli.Services.Background;

// Реализация Dependency Injection на базе консольного приложения.

var builder = Host.CreateEmptyApplicationBuilder(null);
builder.Configuration.AddJsonFile("appsettings.json");

// Подключение основных сервисов программы и формирование конфиг-файла.
builder.Services.AddAppServices(
    new Configuration(
        storageConnectionString: builder.Configuration["StorageConnectionString"] ?? string.Empty,
        minTransactionAmountRub: builder.Configuration.GetValue<int>("MinTransactionAmountRub"),
        maxTransactionAmountRub: builder.Configuration.GetValue<int>("MaxTransactionAmountRub"),
        minTransactionAmountUsd: builder.Configuration.GetValue<int>("MinTransactionAmountUsd"),
        maxTransactionAmountUsd: builder.Configuration.GetValue<int>("MaxTransactionAmountUsd"),
        currencyUsdToRub: builder.Configuration.GetValue<decimal>("CurrencyUsbToRub")));

// Подключение сервисов для:
// > Базового вывода данных в консоль.
// > Консольного логирования.
builder.Services.AddSingleton<IConsole, ServiceConsole>();
builder.Services.AddSingleton<ILogger, LoggerConsole>();

// Подключение сервисов, отвечающих за команды, которые могут быть выполнены в программе.
builder.Services.AddTransient<ICommand, CommandWalletsDelete>();       // Удаление всех кошельков.
builder.Services.AddTransient<ICommand, CommandWalletsGenerate>();     // Создание случайных кошельков.
builder.Services.AddTransient<ICommand, CommandTransactionsDelete>();  // Удаление всех транзакций.
builder.Services.AddTransient<ICommand, CommandTransactionGenerate>(); // Создание случайных транзакций.
builder.Services.AddTransient<ICommand, CommandTaskTransactions>();    // Вывод крупнейших транзакций за месяц.
builder.Services.AddTransient<ICommand, CommandTaskWallets>();         // Вывод крупнейших расходов для всех кошельков.

// Подключение фабрики команд, выполняюшихся программой по запросу пользователя.
builder.Services.AddSingleton<ICommandFactory, CommandFactory>();

// Подключение основного сервиса программы.
// Именно здесь выполняется базовое взаимодействие с пользователем
// и выбор им желаемой операции.
builder.Services.AddHostedService<BackgroundServiceProgram>();

// Непосредственный запуск потока приложения.
using var host = builder.Build();
await host.RunAsync();
