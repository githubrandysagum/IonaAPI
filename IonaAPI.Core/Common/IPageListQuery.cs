using IonaAPI.Core.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IonaAPI.Core.Services.Common
{
    internal interface IPageListQuery<T>
    {
        public Task<PageListCountResult<T>> ExecuteAsync(int page = 0, int limit = 10);
    }
}
