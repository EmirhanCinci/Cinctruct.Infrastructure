namespace Infrastructure.Model.Dtos.Implementations
{
	/// <summary>
	/// Provides a base implementation for Data Transfer Objects (DTOs) with an additional name property.
	/// </summary>
	/// <typeparam name="TDtoId">The type of the DTO's unique identifier.</typeparam>
	public abstract class BasicDto<TDtoId> : BaseDto<TDtoId>
	{
		/// <summary>
		/// Gets or sets the name of the DTO.
		/// Defaults to an empty string.
		/// </summary>
		public string Name { get; set; } = string.Empty;
	}
}
