using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IonaAPI.Core.ApiResult
{
    public class PageListResult<T>
    {
        public PageListResult(int page, int limit)
        {
            Page = page;
            Limit = limit;
            Results = new List<T>();
        }

        public PageListResult(int page, int limit, List<T> list)
        {
            Page = page;
            Limit = limit;
            Results = list;
        }

        public int Page { get; set; }
        public int Limit { get; set; }

        public List<T> Results { get; set; } 

        public void AddRange(PageListResult<T> ranges)
        {
            Results.AddRange(ranges.Results);
        }

        public void AddRange(PageListCountResult<T> ranges)
        {
            Results.AddRange(ranges.Results);
        }

        public void AddRangeTake(PageListCountResult<T> ranges, int take)
        {
            Results.AddRange(ranges.Results.Take(take));
        }

        public void AddRangeSkip(PageListCountResult<T> ranges, int skip)
        {
            Results.AddRange(ranges.Results.Skip(skip));
        }
    }
}
