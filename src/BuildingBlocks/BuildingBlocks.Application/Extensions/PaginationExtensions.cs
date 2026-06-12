using BuildingBlocks.Application.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Application.Extensions
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, PagedQuery request)
        {
            return query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, PagedQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .ApplyPaging(request)
                .ToListAsync(cancellationToken);

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}
