using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Search;

namespace Common
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Arrange the Source by given Sort Model. Within sortModel the property Desc must be specified to true or false for correct ordering the entities
        /// </summary>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<SearchOrderDto> sortModels)
        {
            if (sortModels == null || !sortModels.Any())
            {
                return source;
            }

            try
            {
                var count = 0;
                var expression = source.Expression;

                foreach (var item in sortModels)
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var selector = Expression.PropertyOrField(parameter, item.ColumnName);
                    var method = item.Desc ? (count == 0 ? "OrderByDescending" : "ThenByDescending") : (count == 0 ? "OrderBy" : "ThenBy");

                    expression = Expression.Call(
                                                    typeof(Queryable),
                                                    method,
                                                    new Type[] { source.ElementType, selector.Type },
                                                    expression, Expression.Quote(Expression.Lambda(selector, parameter))
                                                 );
                    count++;
                }

                return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid column name");
            }
        }
    }
}

