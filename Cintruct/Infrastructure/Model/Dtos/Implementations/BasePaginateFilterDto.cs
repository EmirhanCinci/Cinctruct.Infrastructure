namespace Infrastructure.Model.Dtos.Implementations
{
    public abstract class BasePaginateFilterDto : BaseFilterDto
    {
        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
