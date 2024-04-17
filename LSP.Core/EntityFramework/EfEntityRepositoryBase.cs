using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using LSP.Core.Pagination;
using LSP.Core.Repository;

namespace LSP.Core.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using var context = new TContext();
            var addEntity = context.Entry(entity);
            addEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            using var context = new TContext();
            var delEntity = context.Entry(entity);
            delEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext();
            return context.Set<TEntity>().FirstOrDefault(filter);
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new TContext();
            return filter is null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();
        }
        public TEntity GetLast(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, int>> orderby = null)
        {
            using var context = new TContext();
            return context.Set<TEntity>().OrderBy(orderby).LastOrDefault(filter);
        }
        public void Update(TEntity entity)
        {
            using var context = new TContext();
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }

        public PagingResponseDto<TEntity> GetPagingList(PagingRequestDto pagingRequestDto,
            Expression<Func<TEntity, bool>> filters = null, Expression<Func<TEntity, object>> orderby = null)
        {
            using var context = new TContext();
            var filteredData = new List<TEntity>();
            var pagingDto = new PagingDto
            {
                Page = pagingRequestDto.Page,
                Size = pagingRequestDto.Size
            };
            IQueryable<TEntity> data;
            data = filters == null ? context.Set<TEntity>() : context.Set<TEntity>().Where(filters); //filter

            return data.ToPaginate(pagingRequestDto, orderby);
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
                expressions.Add((Expression)DynamicExpressionParser.ParseLambda(new[] { parameter }, null, string.Join(".", x)).Body); //string expression are converted to expression
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

        private IQueryable<TEntity> FilterDataForEqual(IQueryable<TEntity> data, IDictionary<string, object> filterParameters)
        {
            foreach (var filterParameter in filterParameters)
            {
                var expressionForFilter = GetPropertySelectorForFilterEqual<TEntity>(filterParameter);
                data = data.Where(expressionForFilter);
            }
            return data;
        }

        private IQueryable<TEntity> FilterDataForExpression(IQueryable<TEntity> data, List<Expression<Func<TEntity, bool>>> expressions)
        {
            foreach (var expression in expressions)
            {
                data = data.Where(expression);
            }
            return data;
        }

        private List<string> CheckTypeForSearch(List<string> searchTypes, PropertyInfo[] types)
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

        private static List<Expression<Func<TEntity, bool>>> GetExperssionForSecondTableParameters<TEntity>(List<KeyValueParameterDto> parameters, PropertyInfo[] types)
        {
            var finalExpressions = new List<Expression<Func<TEntity, bool>>>();
            foreach (var parameter in parameters)
            {
                if (parameter.Name != null && parameter.Values != null && parameter.Values.Length != 0)
                {
                    var parameterType = types.FirstOrDefault(x => x.Name.Trim().ToLower().Contains(parameter.Name.Trim().ToLower())).Name;

                    var param = Expression.Parameter(typeof(TEntity), "x");
                    var expressions = new List<Expression>();
                    foreach (var value in parameter.Values)
                    {
                        expressions.Add(Expression.Equal(
                        Expression.Property(param, parameterType),
                    Expression.Constant(value)
                        ));
                    }
                    var body = expressions.Skip(1).Aggregate(expressions[0], Expression.OrElse);
                    var expression = Expression.Lambda<Func<TEntity, bool>>(body, new ParameterExpression[] { param });
                    finalExpressions.Add(expression);
                }
            }
            return finalExpressions;
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

        public PagingResponseDto<TEntity> GetPagingList(PagingRequestDto pagingRequestDto,
            Expression<Func<TEntity, object>> orderBy = null, Expression<Func<TEntity, object>> thenOrderBy = null, bool isDesc = false, List<string> searchTypes = null, IDictionary<string, string> stringParameters = null, IDictionary<string, int?> intParameters = null, IDictionary<string, bool?> boolParameters = null, IDictionary<string, byte?> byteParameters = null, List<KeyValueDto> dateTimeParameters = null, List<KeyValueParameterDto> parameters = null, string search = null)
        {
            Type type = typeof(TEntity);
            var types = type.GetProperties(); //getting properties for matching

            searchTypes = searchTypes != null ? CheckTypeForSearch(searchTypes, types) : null;
            var expression = searchTypes != null && search != null ? GetPropertySelectorForSearch<TEntity>(searchTypes, search) : null;
            IDictionary<string, object> castingStringParameters = stringParameters != null ? stringParameters.ToDictionary(item => item.Key, item => (object)item.Value) : null; //Casting  for dictionary
            castingStringParameters = castingStringParameters != null ? castingStringParameters.Count != 0 ? CheckTypeForFilter(castingStringParameters, types) : null : null; //for string parameters
            IDictionary<string, object> castingIntParameters = intParameters != null ? intParameters.ToDictionary(item => item.Key, item => (object)item.Value) : null; //Casting  for dictionary
            castingIntParameters = castingIntParameters != null ? castingIntParameters.Count != 0 ? CheckTypeForFilter(castingIntParameters, types) : null : null; //for int parameters
            IDictionary<string, object> castingBoolParameters = boolParameters != null ? boolParameters.ToDictionary(item => item.Key, item => (object)item.Value) : null; //Casting for dictionary
            castingBoolParameters = castingBoolParameters != null ? castingBoolParameters.Count != 0 ? CheckTypeForFilter(castingBoolParameters, types) : null : null; //for bool parameters
            IDictionary<string, object> castingByteParameters = byteParameters != null ? byteParameters.ToDictionary(item => item.Key, item => (object)item.Value) : null; //Casting  for dictionary
            castingByteParameters = castingByteParameters != null ? castingByteParameters.Count != 0 ? CheckTypeForFilter(castingByteParameters, types) : null : null; //for byte parameters
            dateTimeParameters = dateTimeParameters != null ? dateTimeParameters.Count != 0 ? CheckTypeForFilterDateTimeParameters(dateTimeParameters, types) : null : null; //for datetime parameters
            var dateTimeExpression = dateTimeParameters != null ? dateTimeParameters.Count != 0 ? dateTimeParameters.Count == 1 ? GetPropertySelectorForDatetimeParameter(dateTimeParameters) : GetPropertySelectorForDatetimeParameters(dateTimeParameters) : null : null;
            var parameterExpressions = parameters != null ? GetExperssionForSecondTableParameters<TEntity>(parameters, types) : null;

            var response = new PagingResponseDto<TEntity>();
            var total = (pagingRequestDto.Page - 1) * pagingRequestDto.Size;
            int totalCount = 0;
            using (var context = new TContext())
            {
                var data = context.Set<TEntity>().AsQueryable();
                if (castingStringParameters != null && castingStringParameters.Count != 0)
                {
                    data = FilterDataForEqual(data, castingStringParameters);
                }
                if (castingIntParameters != null && castingIntParameters.Count != 0)
                {
                    data = FilterDataForEqual(data, castingIntParameters);
                }
                if (castingBoolParameters != null && castingBoolParameters.Count != 0)
                {
                    data = FilterDataForEqual(data, castingBoolParameters);
                }
                if (castingByteParameters != null && castingByteParameters.Count != 0)
                {
                    data = FilterDataForEqual(data, castingByteParameters);
                }
                if (expression != null)
                {
                    data = data.Where(expression);
                }
                if (dateTimeExpression != null)
                {
                    data = data.Where(dateTimeExpression);
                }
                if (parameterExpressions != null)
                {
                    data = FilterDataForExpression(data, parameterExpressions);
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

                //if (expression == null)
                //{
                //    int totalCount = context.Set<TEntity>().Count();
                //    int totalPage = (int)Math.Ceiling((decimal)totalCount / pagingRequestDto.Size);
                //    var pageResponse = new PagingDto
                //    {
                //        Page = pagingRequestDto.Page,
                //        Size = pagingRequestDto.Size,
                //        TotalCount = totalCount,
                //        TotalPage = totalPage
                //    };
                //    response.Page = pageResponse;
                //    if (orderBy == null)
                //    {
                //        if (castingStringParameters != null)
                //        {
                //            var data = context.Set<TEntity>().AsQueryable();
                //            response.Data = FilterDataForEqual(data, castingStringParameters).Skip(total).Take(pagingRequestDto.Size).ToList();
                //        }
                //        else if (parameterExpressions != null)
                //        {
                //            var data = context.Set<TEntity>().AsQueryable();
                //            response.Data = isDesc == false ? FilterDataForExpression(data, parameterExpressions).Skip(total).Take(pagingRequestDto.Size).ToList() : FilterDataForExpression(data, parameterExpressions).Skip(total).Take(pagingRequestDto.Size).ToList();
                //        }
                //        else
                //        {
                //            response.Data = context.Set<TEntity>().Skip(total).Take(pagingRequestDto.Size).ToList();
                //        }
                //    }
                //    else
                //    {
                //        if(parameterExpressions != null)
                //        {
                //            var data = context.Set<TEntity>().AsQueryable();
                //            response.Data = isDesc == false ? FilterDataForExpression(data, parameterExpressions).OrderBy(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList() : FilterDataForExpression(data, parameterExpressions).OrderByDescending(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList();
                //        }
                //        else if (castingStringParameters != null)
                //        {
                //            var data = context.Set<TEntity>().AsQueryable();
                //            response.Data = isDesc == false ? FilterDataForEqual(data, castingStringParameters).OrderBy(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList() : FilterDataForEqual(data, castingStringParameters).OrderByDescending(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList();
                //        }
                //        else
                //        {
                //            response.Data = isDesc == false ? context.Set<TEntity>().OrderBy(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList() :
                //                context.Set<TEntity>().OrderByDescending(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList();
                //        }
                //    }
                //    return response;
                //}
                //else
                //{
                //    int totalCount = context.Set<TEntity>().Count();
                //    if (orderBy == null)
                //    {
                //        if (castingStringParameters != null)
                //        {
                //            var data = context.Set<TEntity>().Where(expression);
                //            response.Data = FilterDataForEqual(data, castingStringParameters).Skip(total).Take(pagingRequestDto.Size).ToList();
                //        }
                //        else if(parameterExpressions != null) 
                //        {
                //            var data = context.Set<TEntity>().Where(expression);
                //            response.Data = isDesc == false ? FilterDataForExpression(data, parameterExpressions).Skip(total).Take(pagingRequestDto.Size).ToList() : FilterDataForExpression(data, parameterExpressions).Skip(total).Take(pagingRequestDto.Size).ToList();
                //        }
                //        else
                //        {
                //            response.Data = context.Set<TEntity>().Where(expression).Skip(total).Take(pagingRequestDto.Size).ToList();
                //        }
                //    }
                //    else
                //    {
                //        if (castingStringParameters != null)
                //        {
                //            var data = context.Set<TEntity>().Where(expression);
                //            response.Data = isDesc == false ? FilterDataForEqual(data, castingStringParameters).OrderBy(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList() : FilterDataForEqual(data, castingStringParameters).OrderByDescending(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList();
                //        }
                //        else if(parameterExpressions != null)
                //        {
                //            var data = context.Set<TEntity>().Where(expression);
                //            response.Data = isDesc == false ? FilterDataForExpression(data, parameterExpressions).OrderBy(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList() : FilterDataForExpression(data, parameterExpressions).OrderByDescending(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList();
                //        }
                //        else
                //        {
                //            response.Data = isDesc == false ? context.Set<TEntity>().Where(expression).OrderBy(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList() :
                //            context.Set<TEntity>().Where(expression).OrderByDescending(orderBy).Skip(total).Take(pagingRequestDto.Size).ToList();
                //        }
                //    }

                int totalPage = (int)Math.Ceiling((decimal)totalCount / pagingRequestDto.Size);
                var pageResponse = new PagingDto
                {
                    Page = pagingRequestDto.Page,
                    Size = pagingRequestDto.Size,
                    TotalSize = totalCount,
                    TotalPage = totalPage
                };
                response.Pagination = pageResponse;
                return response;

            }
        }
    }
}
