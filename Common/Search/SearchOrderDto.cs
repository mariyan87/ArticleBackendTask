namespace Common.Search
{
    /// <summary>
    /// Holds sorting info.
    /// </summary>
    public class SearchOrderDto
    {
        /// <summary>
        /// The name of the column to sort by.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// If sorting ascending or descending.
        /// </summary>
        public bool Desc { get; set; }
    }
}
