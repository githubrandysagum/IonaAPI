using IonaAPI.Core.Services.Common;
using IonaAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IonaAPI.Core.Interfaces;
using IonaAPI.Core.ApiResult;

namespace IonaAPI.Core.Queries
{
    internal class GetCatClientImagesQuery : IPageListQuery<Images>
    {
        private ICatService catService { get; set; }

        public GetCatClientImagesQuery(ICatService catService)
        {
            this.catService = catService;
        }

        public Task<PageListCountResult<Images>> ExecuteAsync(int page = 0, int limit = 10)
        {
            if (page < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page));
            }

            if (limit < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(limit));
            }
            return catService.GetImagesAsync(page, limit);  
        }
    }
}
