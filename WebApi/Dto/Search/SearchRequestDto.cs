using System.Collections.Generic;
using Common.Search;

namespace WebApi.Dto.Search
{
    /// <summary>
    /// Represents the standard request for searching objects.
    /// </summary>
    /// <typeparam name="T">The type of the filter used to filter the objects.</typeparam>
    public class SearchRequestDto<T> where T : class
    {
        /// <summary>
        /// Counts of Items per page.
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Current page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// List of columns to order results by.
        /// </summary>
        public List<SearchOrderDto> Order { get; set; }

        /// <summary>
        /// The filter used to search.
        /// </summary>
        public T Filter { get; set; }

    }
}
