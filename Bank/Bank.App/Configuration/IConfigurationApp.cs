using Bank.Storage;

namespace Bank.App.Configuration;

public interface IConfigurationApp : 
    IConfigurationStorage, 
    IConfugurationCurrencies, 
    IConfigurationTransaction { }
