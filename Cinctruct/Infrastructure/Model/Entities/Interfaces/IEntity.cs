namespace Infrastructure.Model.Entities.Interfaces
{
	/// <summary>
	/// Represents a base entity with common properties for entities in the system.
	/// </summary>
	/// <typeparam name="TEntityId">The type of the entity's unique identifier.</typeparam>
	public interface IEntity<TEntityId>
	{
		/// <summary>
		/// Gets or sets the unique identifier of the entity.
		/// </summary>
		TEntityId Id { get; set; }

		/// <summary>
		/// Gets or sets the date and time when the entity was created.
		/// </summary>
		DateTime CreatedDate { get; set; }

		/// <summary>
		/// Gets or sets the date and time when the entity was last updated.
		/// </summary>
		DateTime? UpdatedDate { get; set; }

		/// <summary>
		/// Gets or sets the date and time when the entity was deleted.
		/// </summary>
		DateTime? DeletedDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the entity is deleted.
		/// </summary>
		bool IsDeleted { get; set; }
	}
}
