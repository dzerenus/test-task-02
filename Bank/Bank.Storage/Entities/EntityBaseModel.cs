using Bank.Core.Models;

namespace Bank.Storage.Entities;

internal class EntityBaseModel : IEntity<BaseModel>
{
    public Guid Id { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public EntityBaseModel(BaseModel model)
    {
        Id = model.Id;
        CreatedAtUtc = model.CreatedAtUtc;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public EntityBaseModel(
        Guid id,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
    {
        Id = id;
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = updatedAtUtc;
    }

    public BaseModel GetModel()
    {
        return new BaseModel(
            id: Id,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
