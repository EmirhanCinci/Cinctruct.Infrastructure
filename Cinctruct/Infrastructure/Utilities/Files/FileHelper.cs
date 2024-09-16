using Infrastructure.Model.Dtos.Implementations;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Utilities.Files
{
	/// <summary>
	/// A helper class for file operations, such as saving files to a specified directory.
	/// </summary>
	public class FileHelper
	{
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileHelper"/> class.
		/// </summary>
		/// <param name="configuration">The configuration object used to retrieve file settings.</param>
		public FileHelper(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// Saves a file to the specified directory and returns the URL of the saved file.
		/// </summary>
		/// <param name="fileDto">The data transfer object containing the file data and metadata.</param>
		/// <param name="mainFilePath">The main directory path where the file will be saved.</param>
		/// <returns>The URL of the saved file.</returns>
		public string SaveFile(FileDto fileDto, string mainFilePath)
		{
			byte[] fileBytes = Convert.FromBase64String(fileDto.Base64String);
			string fileExtension = Path.GetExtension(fileDto.Name);
			string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
			string filePath = Path.Combine("files", mainFilePath, uniqueFileName);
			string directoryPath = Path.GetDirectoryName(filePath);
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}
			File.WriteAllBytes(filePath, fileBytes);
			string baseUrl = _configuration.GetSection("FileSettings:BaseUrl").Value;
			string fileUrl = new Uri(new Uri(baseUrl), $"/Files/{mainFilePath}/{uniqueFileName}").ToString();
			return fileUrl;
		}
	}
}
