﻿using CodexBank.Web.Infrastructure.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodexBank.Web.Infrastructure.Extensions
{
    public static class PaginatedListExtension
    {
        private const int DefaultSurroundingPageCount = 3;

        public static PaginatedList<T> ToPaginatedList<T>(
            this IEnumerable<T> items,
            int itemsTotalCount,
            int pageIndex,
            int itemsCountPerPage,
            int surroundingPagesCount = DefaultSurroundingPageCount)
        {
            var itemsArray = items as T[] ?? items.ToArray();

            var totalPages = (int)Math.Ceiling(itemsTotalCount / (double)itemsCountPerPage);
            pageIndex = Math.Min(pageIndex, totalPages);

            return new PaginatedList<T>(itemsArray, pageIndex, totalPages, surroundingPagesCount);
        }
    }
}
