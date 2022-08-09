using IonaAPI.Core.Services.Common;
using IonaAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IonaAPI.Core.Interfaces;
using IonaAPI.Core.ApiResult;

namespace IonaAPI.Core.Services.Queries
{
    internal class GetCatClientImagesByBreedIdQuery : IPageListQuery<BreedImages>
    {
        private ICatService catService { get; set; }
        private string id { get; set; }
        public GetCatClientImagesByBreedIdQuery(ICatService catService, string id)
        {
            this.catService = catService;
            this.id = id;
        }

       
        public Task<PageListCountResult<BreedImages>> ExecuteAsync(int page = 0, int limit = 10)
        {
            if (page < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page));
            }

            if (limit < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(limit));
            }
            return catService.GetImagesByBreedIdAsync(id, page, limit);
        }
    }
}
