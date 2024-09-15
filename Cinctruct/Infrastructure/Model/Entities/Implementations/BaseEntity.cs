using Infrastructure.Model.Entities.Interfaces;

namespace Infrastructure.Model.Entities.Implementations
{
    public abstract class BaseEntity<TEntityId> : IEntity<TEntityId>
    {
        public TEntityId Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
