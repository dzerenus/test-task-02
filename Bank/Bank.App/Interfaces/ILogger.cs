namespace Bank.App.Interfaces;

public interface ILogger
{
    public void Inf(string message);

    public void Wrn(string message);

    public void Err(string message);
}
