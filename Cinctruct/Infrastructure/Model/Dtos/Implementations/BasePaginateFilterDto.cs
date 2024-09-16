namespace Infrastructure.Model.Dtos.Implementations
{
	/// <summary>
	/// Provides a base Data Transfer Object (DTO) for pagination with filtering capabilities.
	/// </summary>
	public abstract class BasePaginateFilterDto : BaseFilterDto
	{
		/// <summary>
		/// Gets or sets the index of the page to retrieve.
		/// Defaults to 1, representing the first page.
		/// </summary>
		public int Index { get; set; } = 1;

		/// <summary>
		/// Gets or sets the number of items to include on each page.
		/// Defaults to 10.
		/// </summary>
		public int Size { get; set; } = 10;
	}
}
