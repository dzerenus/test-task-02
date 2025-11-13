using Bank.Core.Models;

namespace Bank.Storage.Entities;

internal interface IEntity<T> where T : BaseModel
{
    public T GetModel();
}
