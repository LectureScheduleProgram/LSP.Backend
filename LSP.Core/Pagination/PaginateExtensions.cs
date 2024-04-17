using System.Linq.Expressions;

namespace LSP.Core.Pagination
{
    public static class PaginateExtensions
    {
        public static PagingResponseDto<T> ToPaginate<T>(this IQueryable<T> data, PagingRequestDto pagingRequestDto, Expression<Func<T, object>> orderby = null) where T : class, new()
        {
            var pagingDto = new PagingDto
            {
                Page = pagingRequestDto.Page,
                Size = pagingRequestDto.Size
            };

            data = orderby == null ? data : data.OrderByDescending(orderby);

            int count = data.Count();
            pagingDto.TotalSize = data.Count();
            pagingDto.TotalPage = (int)Math.Ceiling((decimal)pagingDto.TotalSize / pagingRequestDto.Size);
            List<T> items = data.Skip((pagingRequestDto.Page - 1) * pagingRequestDto.Size).Take(pagingRequestDto.Size).ToList();
            pagingDto.Prev = pagingDto.Page > 1;
            pagingDto.Next = pagingDto.Page < pagingDto.TotalPage;
            PagingResponseDto<T> response = new()
            {
                Data = items,
                Pagination = pagingDto
            };
            return response;
        }
    }
}
