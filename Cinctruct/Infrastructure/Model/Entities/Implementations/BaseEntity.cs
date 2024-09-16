using Infrastructure.Model.Entities.Interfaces;

namespace Infrastructure.Model.Entities.Implementations
{
	/// <summary>
	/// Provides a base implementation for entities with common properties.
	/// </summary>
	/// <typeparam name="TEntityId">The type of the entity's unique identifier.</typeparam>
	public abstract class BaseEntity<TEntityId> : IEntity<TEntityId>
	{
		/// <summary>
		/// Varlığın benzersiz tanımlayıcısını alır veya ayarlar.
		/// </summary>
		public TEntityId Id { get; set; }

		/// <summary>
		/// Gets or sets the date and time when the entity was created.
		/// Defaults to the current date and time when the instance is created.
		/// </summary>
		public DateTime CreatedDate { get; set; } = DateTime.Now;

		/// <summary>
		/// Gets or sets the date and time when the entity was last updated.
		/// </summary>
		public DateTime? UpdatedDate { get; set; }

		/// <summary>
		/// Gets or sets the date and time when the entity was deleted.
		/// </summary>
		public DateTime? DeletedDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the entity is deleted.
		/// Defaults to false, indicating that the entity is not deleted.
		/// </summary>
		public bool IsDeleted { get; set; } = false;
	}
}
