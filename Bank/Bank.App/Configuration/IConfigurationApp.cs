using Bank.Storage;

namespace Bank.App.Configuration;

/// <summary>
/// Главная конфигурация приложения.
/// </summary>
public interface IConfigurationApp : 
    IConfigurationStorage, 
    IConfugurationCurrencies, 
    IConfigurationTransaction { }
