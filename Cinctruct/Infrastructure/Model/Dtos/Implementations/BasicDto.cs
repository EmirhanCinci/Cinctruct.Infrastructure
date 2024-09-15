namespace Infrastructure.Model.Dtos.Implementations
{
    public abstract class BasicDto<TDtoId> : BaseDto<TDtoId>
    {
        public string Name { get; set; } = string.Empty;
    }
}
