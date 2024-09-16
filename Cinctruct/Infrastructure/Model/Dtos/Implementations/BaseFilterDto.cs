using Infrastructure.Model.Dtos.Interfaces;

namespace Infrastructure.Model.Dtos.Implementations
{
	/// <summary>
	/// Provides a base Data Transfer Object (DTO) for filtering data with a deletion status.
	/// </summary>
	public abstract class BaseFilterDto : IDto
	{
		public bool? IsDeleted { get; set; }
	}
}
