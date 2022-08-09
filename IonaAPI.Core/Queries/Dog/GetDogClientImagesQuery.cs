﻿using IonaAPI.Core.Services.Common;
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
    internal class GetDogClientImagesQuery : IPageListQuery<Images>
    {
        private IDogService dogService { get; set; }

        public GetDogClientImagesQuery(IDogService dogService)
        {
            this.dogService = dogService;
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
            return dogService.GetImagesAsync(page, limit);  
        }
    }
}
