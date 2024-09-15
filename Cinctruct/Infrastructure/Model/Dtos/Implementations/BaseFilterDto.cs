using Infrastructure.Model.Dtos.Interfaces;

namespace Infrastructure.Model.Dtos.Implementations
{
    public abstract class BaseFilterDto : IDto
    {
        public bool? IsDeleted { get; set; }
    }
}
