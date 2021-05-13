using System;
using System.Linq;
using System.Linq.Expressions;

namespace Projection.Domain.SearchFilter.Extensions
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<T> GenericSort<T>(this IQueryable<T> source, string filterSortBy, Func<IQueryable<T>, IOrderedQueryable<T>> defaultSort) where T : class
        {
            if (string.IsNullOrWhiteSpace(filterSortBy))
                return defaultSort(source);

            var sortExpressionPropNames = filterSortBy
                .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLower())
                .ToList();

            var desc = sortExpressionPropNames.Last().ToLower() == "desc";

            var typeProps = typeof(T).GetProperties();

            var fieldNames = typeProps
                .Where(x => sortExpressionPropNames.Contains(x.Name.ToLower()))
                .ToList();

            if (!fieldNames.Any())
                return defaultSort(source);

            foreach (var fieldName in fieldNames)
            {
                var param = Expression.Parameter(typeof(T));
                var exp = Expression.Property(param, fieldName);
                var lambda = Expression.Lambda(exp, param);
                source = source.Sort(lambda, desc);
            }

            return source as IOrderedQueryable<T>;
        }

        private static IOrderedQueryable<T> Sort<T>(this IQueryable<T> source,
            LambdaExpression exp,
            bool desc = false)
        {
            if (source.IsOrdered())
            {
                return desc
                    ? Queryable.ThenByDescending((IOrderedQueryable<T>)source, (dynamic)exp)
                    : Queryable.ThenBy((IOrderedQueryable<T>)source, (dynamic)exp);
            }

            return desc
                ? Queryable.OrderByDescending(source, (dynamic)exp)
                : Queryable.OrderBy(source, (dynamic)exp);
        }

        private static bool IsOrdered<T>(this IQueryable<T> queryable)
            => queryable.Expression.Type == typeof(IOrderedQueryable<T>);
    }
}