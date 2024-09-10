using Infrastructure.Model.Dtos.Implementations;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Utilities.Files
{
    public class FileHelper
    {
        private readonly IConfiguration _configuration;
        public FileHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
