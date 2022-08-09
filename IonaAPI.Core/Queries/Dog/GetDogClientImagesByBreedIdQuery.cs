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
    internal class GetDogClientImagesByBreedIdQuery : IPageListQuery<BreedImages>
    {
        private IDogService dogService { get; set; }
        private string id { get; set; }
        public GetDogClientImagesByBreedIdQuery(IDogService dogService, string id)
        {
            this.dogService = dogService;
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
            return dogService.GetImagesByBreedIdAsync(id, page, limit);
        }
    }
}
