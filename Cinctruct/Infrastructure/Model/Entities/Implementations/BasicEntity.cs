namespace Infrastructure.Model.Entities.Implementations
{
	/// <summary>
	/// Represents a base entity with an additional name property.
	/// </summary>
	/// <typeparam name="TEntityId">The type of the entity's unique identifier.</typeparam>
	public abstract class BasicEntity<TEntityId> : BaseEntity<TEntityId>
	{
		/// <summary>
		/// Gets or sets the name of the entity.
		/// Defaults to an empty string.
		/// </summary>
		public string Name { get; set; } = string.Empty;
	}
}
