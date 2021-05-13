using System;

namespace Projection.Domain.SearchFilter.Extensions
{
    public static class FilterMapExtensions
    {
        public static T MapString<T>(this string source, Func<string, T> mapper) where T : class
        {
            return string.IsNullOrWhiteSpace(source) ? null : mapper(source);
        }
    }
}
