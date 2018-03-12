using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTal.JoggingApp.Service.Models.Helpers
{
    public class GridPagingInfo : Filter
    {
        public int? FirstRow { get; set; }

        public int? RowCount { get; set; }

        /// <summary>
        /// '|' separated list of fieldnames
        /// </summary>
        public string[] OrderBy { get; set; }

        public bool OrderByDescending { get; set; }

        /// <summary>
        /// Sets total row count into TotalRowCount, if specified
        /// </summary>
        public bool ReturnTotalRowCount { get; set; }

        public int TotalRowCount { get; set; }

        public GridPagingInfo()
        {
        }

        public T ToEntity<T>() where T : BusinessEntities.Helpers.GridPagingInfo, new()
        {
            return new T()
            {
                FirstRow = this.FirstRow,
                RowCount = this.RowCount,
                OrderBy = this.OrderBy.ToArray(), // .Split('|'),
                OrderByDescending = this.OrderByDescending,
                ReturnTotalRowCount = this.ReturnTotalRowCount,
                TotalRowCount = this.TotalRowCount
            };
        }
    }
}
