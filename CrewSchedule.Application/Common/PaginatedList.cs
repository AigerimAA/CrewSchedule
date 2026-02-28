using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Application.Common
{
    /// <summary>
    /// Обёртка для постраничной выдачи данных
    /// Содержит список элементов текущей страницы и метаданные о пагинации
    /// </summary>
    public class PaginatedList<T>
    {
        public List<T> Items { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;

        public PaginatedList(List<T> items, int totalCount, int page, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
        }

    }
}
