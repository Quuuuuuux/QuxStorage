namespace DomainLib.Entities;

/// <summary>
/// BaseEntity class describes base fields of entity
/// </summary>
public abstract class BaseEntity: IEquatable<BaseEntity>
{
    /// <summary>
    /// Primary key - unique identifier Guid
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Date of creation
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date of update
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Determining equality of one hundred entities through equality of entity IDs
    /// </summary>
    /// <param name="otherEntity">entity to be compared</param>
    /// <returns></returns>
    public virtual bool Equals(BaseEntity? otherEntity) => otherEntity != null && Id.Equals(otherEntity.Id);
}