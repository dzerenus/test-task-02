using Microsoft.Extensions.DependencyInjection;
using Bank.Storage;
using Bank.App.Services;
using Bank.App.Interfaces;
using Bank.App.Configuration;

namespace Bank.App;

/// <summary>
/// Основные сервисы приложения, на основе которых осуществляется работа с программой,
/// использование этих сервисов гарантируют правильную логику работы программы.
/// </summary>
public static class AppServices
{
    /// <summary>
    /// Подключение сервисов.
    /// </summary>
    /// <param name="services">Другие сервисы программы.</param>
    /// <param name="configuration">Конфигурация программы.</param>
    /// <returns>Другие сервисы, к которым добавлены основные сервисы программы.</returns>
    public static IServiceCollection AddAppServices(
        this IServiceCollection services, 
        IConfigurationApp configuration)
    {
        // Подключение конфигураций.
        // + Конфигурация хранилища (базы данных).
        // + Конфигурация курсов валют.
        // + Конфигурация транзакций.
        services.AddSingleton<IConfigurationStorage>(configuration);     
        services.AddSingleton<IConfugurationCurrencies>(configuration);  
        services.AddSingleton<IConfigurationTransaction>(configuration); 

        // Подключение сервиса-хранилища.
        // Через этот сервис осуществляется работа с базой данных.
        services.AddSingleton<IServiceStorage, ServiceStorage>();

        // Подкючение основных сервисов приложения.
        // + Сервис управления кошельками.
        // + Сервис управления балансом.
        // + Сервис курсов валют.
        // + Сервис управления транзакциями.
        services.AddSingleton<IServiceWallet, ServiceWallet>();
        services.AddSingleton<IServiceBalance, ServiceBalance>();
        services.AddSingleton<IServiceExchange, ServiceExchange>();
        services.AddSingleton<IServiceTransaction, ServiceTransaction>();

        // Подключение вспомогательных сервисов.
        // + Сервис генерации случайных данных.
        services.AddSingleton<IServiceRandom, ServiceRandom>();

        return services;
    }
}
