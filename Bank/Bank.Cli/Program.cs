using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Bank.App;
using Bank.Cli;
using Bank.Cli.Interfaces;
using Bank.Cli.Services;
using Bank.App.Interfaces;
using Bank.Cli.Services.Background;
using Bank.Cli.Commands;

var builder = Host.CreateEmptyApplicationBuilder(null);
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddAppServices(
    new Configuration(
        storageConnectionString: builder.Configuration["StorageConnectionString"] ?? string.Empty,
        minTransactionAmountRub: builder.Configuration.GetValue<int>("MinTransactionAmountRub"),
        maxTransactionAmountRub: builder.Configuration.GetValue<int>("MaxTransactionAmountRub"),
        minTransactionAmountUsd: builder.Configuration.GetValue<int>("MinTransactionAmountUsd"),
        maxTransactionAmountUsd: builder.Configuration.GetValue<int>("MaxTransactionAmountUsd"),
        currencyUsdToRub: builder.Configuration.GetValue<decimal>("CurrencyUsbToRub")));

builder.Services.AddSingleton<IConsole, ServiceConsole>();
builder.Services.AddSingleton<ILogger, LoggerConsole>();

builder.Services.AddTransient<ICommand, CommandWalletsDelete>();
builder.Services.AddTransient<ICommand, CommandWalletsGenerate>();
builder.Services.AddTransient<ICommand, CommandTransactionsDelete>();
builder.Services.AddTransient<ICommand, CommandTransactionGenerate>();
builder.Services.AddTransient<ICommand, CommandTaskTransactions>();
builder.Services.AddTransient<ICommand, CommandTaskWallets>();

builder.Services.AddSingleton<ICommandFactory, CommandFactory>();

builder.Services.AddHostedService<BackgroundServiceProgram>();

using var host = builder.Build();
await host.RunAsync();

