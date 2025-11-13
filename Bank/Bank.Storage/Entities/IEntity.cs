using Bank.Core.Models;

namespace Bank.Storage.Entities;

/// <summary>
/// Базовая сущность базы данных.
/// </summary>
/// <typeparam name="T">Корневой объект приложения.</typeparam>
internal interface IEntity<T> where T : BaseModel
{
    public T GetModel();
}
