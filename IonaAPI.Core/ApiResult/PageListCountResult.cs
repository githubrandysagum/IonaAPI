using IonaAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IonaAPI.Core.ApiResult
{
    public class PageListCountResult<T>
    {

        public PageListCountResult()
        {

        }
        public PageListCountResult(int page, int limit)
        {
            Page = page;
            Limit = limit; 
            PageCount = 0;
            Results = new List<T>();
        }

        public PageListCountResult(int page, int limit, List<T> breeds) 
        {
            Page = page;
            Limit = limit;
            Results = breeds;
        }

        public int Page { get; set; }
        public int Limit { get; set; }
        public int PageCount { get; set; }

        public int ResultCount { 
            get { 
                return Results.Count;
            } 
        }

        public List<T> Results { get; set; } = new List<T>();

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
