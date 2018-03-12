namespace TopTal.JoggingApp.BusinessEntities.Helpers
{
    public class GridPagingInfo : Filter
    {
        public int? FirstRow { get; set; }

        public int? RowCount { get; set; }

        public string[] OrderBy { get; set; }

        public bool OrderByDescending { get; set; }

        /// <summary>
        /// Sets total row count into TotalRowCount, if specified
        /// </summary>
        public bool ReturnTotalRowCount { get; set; }

        public int TotalRowCount { get; set; }
    }
}
