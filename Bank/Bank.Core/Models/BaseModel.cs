namespace Bank.Core.Models;

// В основе корневых моделей приложения - иммутабельность.
// Изменять уже созданные объекты - нельзя, но можно скопировать
// их с изменением желаемых данных.

/// <summary>
/// Базовая корневая модель приложения.
/// </summary>
public class BaseModel
{
    /// <summary>
    /// ID объекта в базе данных.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Дата создания объекта по UTC.
    /// </summary>
    public DateTime CreatedAtUtc { get; }

    /// <summary>
    /// Дата последнего изменения объекта по UTC.
    /// </summary>
    public DateTime UpdatedAtUtc { get; }

    /// <summary>
    /// Создание нового объекта, которого ранее не было в системе.
    /// </summary>
    public BaseModel()
    {
        Id = Guid.NewGuid();
        CreatedAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    /// <summary>
    /// Копирование объекта для изменения каких-то данных.
    /// </summary>
    /// <param name="model">Копируемый объект.</param>
    public BaseModel(BaseModel model)
    {
        Id = model.Id;
        CreatedAtUtc = model.CreatedAtUtc;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    /// <summary>
    /// Восстановление объекта.
    /// </summary>
    /// <param name="id">ID объекта.</param>
    /// <param name="createdAtUtc">Дата создания объекта по UTC.</param>
    /// <param name="updatedAtUtc">Дата последнего изменения объекта по UTC.</param>
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
