namespace Infrastructure.Model.Entities.Implementations
{
    public abstract class BasicEntity<TEntityId> : BaseEntity<TEntityId>
    {
        public string Name { get; set; } = string.Empty;
    }
}
