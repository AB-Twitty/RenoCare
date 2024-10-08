﻿using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Helpers;
using RenoCare.Core.Helpers.Contracts;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Extensions
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Converts the specified source to <see cref="IPagedList{T}"/> by the specified <paramref name="pageIndex"/> and <paramref name="pageSize"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="source">The source to paging.</param>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <param name="indexFrom">The start index value.</param>
        /// <returns>An instance of the inherited from <see cref="IPagedList{T}"/> interface.</returns>
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source,
            int pageIndex, int pageSize, int totalCount = 0, int indexFrom = 1, int filterCount = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }
            filterCount = filterCount > 0 ? filterCount : await source.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await source.Skip((pageIndex - indexFrom) * pageSize)
                                    .Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);
            var pagedList = new PagedList<T>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                IndexFrom = indexFrom,
                TotalCount = totalCount > 0 ? totalCount : filterCount,
                FilterCount = filterCount,
                Items = items,
                TotalPages = (int)Math.Ceiling(filterCount / (double)pageSize)
            };
            return pagedList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static IQueryable<T> FilterQuery<T>(this IQueryable<T> source, ISearchable search)
        {
            if (search.SearchDict == null || search.SearchDict?.Count <= 0)
                return source;

            foreach (var searchControl in search.SearchDict)
            {
                var propertyName = searchControl.Key;
                var stringValue = searchControl.Value;

                var type = typeof(T);
                var property = type.GetProperty(propertyName) ?? null;

                if (property == null)
                    continue;

                try
                {
                    var filterValues = stringValue.Split(',');

                    if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                    {
                        bool boolValue;
                        if (bool.TryParse(filterValues[0], out boolValue))
                        {
                            source = source.Where($"{property.Name} == @0", boolValue);
                        }
                    }
                    else
                    {
                        if (filterValues.Length == 1)
                        {
                            var qry = $"{property.Name}.ToLower().Contains(\"{filterValues[0].ToLower()}\")";
                            source = source.Where(qry);
                        }
                        else
                        {
                            source = source.Where($"@0.Contains({property.Name})", stringValue);
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }

            return source;
        }

    }
}

