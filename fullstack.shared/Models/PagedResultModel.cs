using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fullstack.shared.Models
{
    public class PagedResultModel<T>
        where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long TotalPages { get; set; }
        public IEnumerable<T> Items { get; set; }
        public long TotalItems { get; set; }
    }
}