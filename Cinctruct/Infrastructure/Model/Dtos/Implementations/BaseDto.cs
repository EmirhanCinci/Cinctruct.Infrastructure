using Infrastructure.Model.Dtos.Interfaces;

namespace Infrastructure.Model.Dtos.Implementations
{
    public abstract class BaseDto<TDtoId> : IDto
    {
        public TDtoId Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
