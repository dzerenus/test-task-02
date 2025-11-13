using Microsoft.Extensions.DependencyInjection;
using Bank.Storage;
using Bank.App.Services;
using Bank.App.Interfaces;
using Bank.App.Configuration;

namespace Bank.App;

public static class AppServices
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services, 
        IConfigurationApp configuration)
    {
        services.AddSingleton<IConfigurationStorage>(configuration);
        services.AddSingleton<IConfugurationCurrencies>(configuration);
        services.AddSingleton<IConfigurationTransaction>(configuration);

        services.AddSingleton<IServiceStorage, ServiceStorage>();

        services.AddSingleton<IServiceBalance, ServiceBalance>();
        services.AddSingleton<IServiceExchange, ServiceExchange>();

        services.AddSingleton<IServiceWallet, ServiceWallet>();
        services.AddSingleton<IServiceTransaction, ServiceTransaction>();

        services.AddSingleton<IServiceRandom, ServiceRandom>();

        return services;
    }
}
