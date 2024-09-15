namespace Infrastructure.Model.Entities.Interfaces
{
    public interface IEntity<TEntityId>
    {
        TEntityId Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
        DateTime? DeletedDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
