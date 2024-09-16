using Infrastructure.Model.Dtos.Interfaces;

namespace Infrastructure.Model.Dtos.Implementations
{
	/// <summary>
	/// Provides a base implementation for Data Transfer Objects (DTOs) with common properties.
	/// </summary>
	/// <typeparam name="TDtoId">The type of the DTO's unique identifier.</typeparam>
	public abstract class BaseDto<TDtoId> : IDto
	{
		/// <summary>
		/// Gets or sets the unique identifier of the DTO.
		/// </summary>
		public TDtoId Id { get; set; }

		/// <summary>
		/// Gets or sets the date and time when the DTO was created.
		/// </summary>
		public DateTime CreatedDate { get; set; }

		/// <summary>
		/// Gets or sets the date and time when the DTO was last updated.
		/// Nullable to allow for the possibility that the DTO has not been updated yet.
		/// </summary>
		public DateTime? UpdatedDate { get; set; }

		/// <summary>
		/// Gets or sets the date and time when the DTO was deleted.
		/// DTO'nun silindiği tarih ve saati alır veya ayarlar.
		/// Nullable to allow for the possibility that the DTO has not been deleted yet.
		/// </summary>
		public DateTime? DeletedDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the DTO is deleted.
		/// </summary>
		public bool IsDeleted { get; set; }
	}
}
