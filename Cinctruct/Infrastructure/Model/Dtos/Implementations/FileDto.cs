using Infrastructure.Model.Dtos.Interfaces;

namespace Infrastructure.Model.Dtos.Implementations
{
    public class FileDto : IDto
    {
        public string Name { get; set; } = string.Empty;
        public string Base64String { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
    }
}
