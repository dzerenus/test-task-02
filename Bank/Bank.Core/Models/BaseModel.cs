namespace Bank.Core.Models;

public class BaseModel
{
    public Guid Id { get; }

    public DateTime CreatedAtUtc { get; }

    public DateTime UpdatedAtUtc { get; }

    public BaseModel()
    {
        Id = Guid.NewGuid();
        CreatedAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public BaseModel(BaseModel model)
    {
        Id = model.Id;
        CreatedAtUtc = model.CreatedAtUtc;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public BaseModel(
        Guid id,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
    {
        Id = id;
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = updatedAtUtc;
    }
}
