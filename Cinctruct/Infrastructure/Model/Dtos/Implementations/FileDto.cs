using Infrastructure.Model.Dtos.Interfaces;

namespace Infrastructure.Model.Dtos.Implementations
{
	/// <summary>
	/// Represents a Data Transfer Object (DTO) for file-related data.
	/// </summary>
	public class FileDto : IDto
	{
		/// <summary>
		/// Gets or sets the name of the file.
		/// Defaults to an empty string.
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the file content encoded as a Base64 string.
		/// Defaults to an empty string.
		/// </summary>
		public string Base64String { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the MIME type of the file.
		/// Defaults to an empty string.
		/// </summary>
		public string ContentType { get; set; } = string.Empty;
	}
}
