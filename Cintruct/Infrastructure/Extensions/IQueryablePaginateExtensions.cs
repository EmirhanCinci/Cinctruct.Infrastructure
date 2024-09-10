using Infrastructure.Utilities.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class IQueryablePaginateExtensions
    {
        public static async Task<Paginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size, CancellationToken cancellationToken = default)
        {
            int count = await source.CountAsync(cancellationToken);
            List<T> items = await source.Skip((index - 1) * size).Take(size).ToListAsync(cancellationToken);
            Paginate<T> list = new() { Index = index, Count = count, Items = items, Size = size, Pages = (int)Math.Ceiling(count / (double)size) };
            return list;
        }

        public static Paginate<T> ToPaginate<T>(this IQueryable<T> source, int index, int size)
        {
            int count = source.Count();
            var items = source.Skip((index - 1) * size).Take(size).ToList();
            Paginate<T> list = new() { Index = index, Size = size, Count = count, Items = items, Pages = (int)Math.Ceiling(count / (double)size) };
            return list;
        }
    }
}
