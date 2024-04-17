using LSP.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace LSP.Core.Pagination.Procedure
{
    public class PagingProcedure<TEntity> : IPagingProcedure<TEntity> where TEntity : class, IDto, new()
    {
        private List<string> CheckTypeForSearch(List<string> searchTypes, System.Reflection.PropertyInfo[] types)
        {
            var newList = new List<string>();
            foreach (var searchType in searchTypes)
            {
                if (types.Any(x => x.Name == searchType)) //matching operation
                {
                    newList.Add(searchType);
                }
            }
            return newList;
        }

        private IDictionary<string, object> CheckTypeForFilter(IDictionary<string, object> filterParameters, System.Reflection.PropertyInfo[] types)
        {
            var newList = new Dictionary<string, object>();
            foreach (var filterParameter in filterParameters)
            {
                if (types.Any(x => x.Name == filterParameter.Key) && filterParameter.Value != null) //matching operation
                {
                    newList.Add(filterParameter.Key, filterParameter.Value);
                }
            }
            return newList;
        }

        private List<KeyValueDto> CheckTypeForFilterDateTimeParameters(List<KeyValueDto> dateTimeParameters, System.Reflection.PropertyInfo[] types)
        {
            var newList = new List<KeyValueDto>();
            foreach (var dateTimeParameter in dateTimeParameters)
            {
                if (types.Any(x => x.Name == dateTimeParameter.Parameter) && (DateTime)dateTimeParameter.Value != DateTime.MinValue)
                {
                    newList.Add(new KeyValueDto { Key = dateTimeParameter.Key, Value = dateTimeParameter.Value, Parameter = dateTimeParameter.Parameter });
                }
            }
            return newList;
        }

        private static Expression<Func<TEntity, bool>> GetPropertySelectorForSearch<TEntity>(List<string> propertyNames, string value)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var components = propertyNames.Select(filter => Expression.Call(
                Expression.Property(parameter, filter),
                typeof(string).GetMethod(nameof(string.Contains), new Type[] { typeof(string) }), //contains method
                Expression.Constant(value.Trim().ToLower())
                )).Cast<Expression>().ToList();
            var expressions = new List<Expression>();
            foreach (var component in components)
            {
                var x = component.ToString().Split("."); //split for adding trim and lower method
                x[1] = x[1].Insert(x[1].Length, ".Trim().ToLower()"); //trim and lower are added as string
                expressions.Add((Expression)DynamicExpressionParser.ParseLambda(new[] { parameter }, null, String.Join(".", x)).Body); //string expression are converted to expression
            }
            var body = expressions.Skip(1).Aggregate(expressions[0], Expression.OrElse);
            return Expression.Lambda<Func<TEntity, bool>>(body, new ParameterExpression[] { parameter });
        }

        private static Expression<Func<TEntity, bool>> GetPropertySelectorForFilterEqual<TEntity>(KeyValuePair<string, object> filterParameter)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var body = Expression.Equal(
                Expression.Property(parameter, filterParameter.Key),
                Expression.Constant(filterParameter.Value)
                );
            return Expression.Lambda<Func<TEntity, bool>>(body, new ParameterExpression[] { parameter });
        }

        private Expression<Func<TEntity, bool>> GetPropertySelectorForDatetimeParameters(List<KeyValueDto> filterParameters)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var start = filterParameters.FirstOrDefault(x => x.Key.Contains("Greater"));
            var end = filterParameters.FirstOrDefault(x => x.Key.Contains("Less"));
            var body = Expression.AndAlso(
                Expression.GreaterThanOrEqual(
                Expression.Property(parameter, start.Parameter),
                Expression.Constant(start.Value)),
                Expression.LessThanOrEqual(
                Expression.Property(parameter, end.Parameter),
                Expression.Constant(end.Value))
                );
            return Expression.Lambda<Func<TEntity, bool>>(body, new ParameterExpression[] { parameter });
        }

        private Expression<Func<TEntity, bool>> GetPropertySelectorForDatetimeParameter(List<KeyValueDto> filterParameters)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            if (filterParameters.Any(x => x.Key.Contains("Greater")))
            {
                var start = filterParameters.FirstOrDefault(x => x.Key.Contains("Greater"));
                var body = Expression.GreaterThanOrEqual(
                    Expression.Property(parameter, start.Parameter),
                    Expression.Constant(start.Value)
                    );
                return Expression.Lambda<Func<TEntity, bool>>(body, new ParameterExpression[] { parameter });
            }
            else
            {
                var end = filterParameters.FirstOrDefault();
                var body = Expression.LessThanOrEqual(
                   Expression.Property(parameter, end.Parameter),
                   Expression.Constant(end.Value)
                   );
                return Expression.Lambda<Func<TEntity, bool>>(body, new ParameterExpression[] { parameter });
            }
        }

        private IQueryable<TEntity> FilterDataForEqual(IQueryable<TEntity> data, IDictionary<string, object> filterParameters)
        {
            foreach (var filterParameter in filterParameters)
            {
                var expressionForFilter = GetPropertySelectorForFilterEqual<TEntity>(filterParameter);
                data = data.Where(expressionForFilter);
            }
            return data;
        }

        public PagingProcedureResponseDto<TEntity> GetPagingList(PagingRequestDto pagingRequestDto,
            Expression<Func<TEntity, object>> orderBy = null, Expression<Func<TEntity, object>> thenOrderBy = null, bool isDesc = false, List<string> searchTypes = null, IDictionary<string, string> stringParameters = null, IDictionary<string, int?> intParameters = null, List<KeyValueDto> dateTimeParameters = null, IQueryable<TEntity> list = null, string search = null)
        {
            Type type = typeof(TEntity);
            var types = type.GetProperties(); //getting properties for matching

            searchTypes = searchTypes != null ? CheckTypeForSearch(searchTypes, types) : null;
            var expression = searchTypes != null && search != null ? GetPropertySelectorForSearch<TEntity>(searchTypes, search) : null;
            IDictionary<string, object> castingStringParameters = stringParameters != null ? stringParameters.ToDictionary(item => item.Key, item => (object)item.Value) : null; //Casting  for dictionary
            castingStringParameters = castingStringParameters != null ? castingStringParameters.Count != 0 ? CheckTypeForFilter(castingStringParameters, types) : null : null; //for string parameters
            IDictionary<string, object> castingIntParameters = intParameters != null ? intParameters.ToDictionary(item => item.Key, item => (object)item.Value) : null; //Casting  for dictionary
            castingIntParameters = castingIntParameters != null ? castingIntParameters.Count != 0 ? CheckTypeForFilter(castingIntParameters, types) : null : null; //for int parameters

            dateTimeParameters = dateTimeParameters != null ? dateTimeParameters.Count != 0 ? CheckTypeForFilterDateTimeParameters(dateTimeParameters, types) : null : null; //for datetime parameters
            var dateTimeExpression = dateTimeParameters != null ? dateTimeParameters.Count != 0 ? dateTimeParameters.Count == 1 ? GetPropertySelectorForDatetimeParameter(dateTimeParameters) : GetPropertySelectorForDatetimeParameters(dateTimeParameters) : null : null;


            var response = new PagingProcedureResponseDto<TEntity>();
            var total = (pagingRequestDto.Page - 1) * pagingRequestDto.Size;
            int totalCount = 0;

            var data = list;

            if (castingStringParameters != null && castingStringParameters.Count != 0)
            {
                data = FilterDataForEqual(data, castingStringParameters);
            }

            if (castingIntParameters != null && castingIntParameters.Count != 0)

            {
                data = FilterDataForEqual(data, castingIntParameters);
            }
            if (expression != null)
            {
                data = data.Where(expression);
            }
            if (dateTimeExpression != null)
            {
                data = data.Where(dateTimeExpression);
            }
            if (orderBy == null)
            {
                totalCount = data.Count();
                response.Data = data.Skip(total).Take(pagingRequestDto.Size).ToList();
            }
            else
            {
                if (thenOrderBy == null)
                {
                    totalCount = data.Count();
                    response.Data = isDesc == false ? data.OrderBy(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList() : data.OrderByDescending(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList();
                }
                else
                {
                    totalCount = data.Count();
                    response.Data = isDesc == false ? data.OrderBy(orderBy).ThenBy(thenOrderBy).Skip(total).Take(pagingRequestDto.Size).ToList() : data.OrderByDescending(orderBy).ThenByDescending(thenOrderBy).Skip(total).Take(pagingRequestDto.Size).ToList();
                }
            }

            int totalPage = (int)Math.Ceiling((decimal)totalCount / pagingRequestDto.Size);
            var pageResponse = new PagingDto
            {
                Page = pagingRequestDto.Page,
                Size = pagingRequestDto.Size,
                TotalSize = totalCount,
                TotalPage = totalPage
            };
            response.Page = pageResponse;
            return response;
        }
    }
}
