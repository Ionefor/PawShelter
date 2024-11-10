using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PawShelter.Application.Models;

namespace PawShelter.Application.Extensions;

public static class QueriesExtensions
{
    public static async Task<PageList<T>> ToPagedList<T>(
        this IQueryable<T> source,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var totalCount = await source.CountAsync(cancellationToken);
        
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PageList<T>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        var a = predicate.Name;
        return condition ? source.Where(predicate) : source;
    }
    
    public static IQueryable<TSource> SortIf<TSource, TKey>(
        this IQueryable<TSource> source,
        bool condition,
        Expression<Func<TSource, TKey>> selector)
    {
        return condition ? source.OrderBy(selector) : source;
    }

    public static IQueryable<TSource> ApplyFilter<TSource>(
        this IQueryable<TSource> source,
        object? filterParams)
    {
        if (filterParams is null)
            return source;
        
        var type = filterParams.GetType();
        
        var properties = type.GetProperties(
            BindingFlags.Public | BindingFlags.Instance);
        
        foreach (var property in properties)
        {
            var value = property.GetValue(filterParams);

            if(value is null)
                continue;
            
            if (!property.PropertyType.IsPrimitive &&
                property.PropertyType != typeof(string) && 
                property.PropertyType != typeof(Guid?))
            {
                source = source.ApplyFilter<TSource>(value);
            }
            else
            {
                var propertyName = property.Name;
                var parameter = Expression.Parameter(typeof(TSource), "x");
                var member = Expression.Property(parameter, propertyName);
                var constant = Expression.Constant(value);
                var equality = Expression.Equal(member, constant);
                var lambda = Expression.Lambda<Func<TSource, bool>>(equality, parameter);

                source = source.Where(lambda);
            } 
        }
        
        return source;
    }
    public static IQueryable<TSource> ApplySorting<TSource>(
        this IQueryable<TSource> source,
        object? sortingParams)
    {
        if (sortingParams is null)
            return source;
        
        var type = sortingParams.GetType();
        var properties = type.GetProperties(
            BindingFlags.Public | BindingFlags.Instance);
        
        IOrderedQueryable<TSource>? orderedQuery = null;
        
        foreach (var property in properties)
        {
            var value = property.GetValue(sortingParams);

            if(value is null)
                continue;
            
            if((bool)value == false)
                continue;
            
            var parameter = Expression.Parameter(typeof(TSource), "x");
            var propertyAccess = Expression.Property(parameter, property.Name);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            
            PropertyInfo? propertyInfo = typeof(TSource).
                GetProperty(property.Name);
            
            if(propertyInfo is null)
                continue;
            
            Type propertyType = propertyInfo.PropertyType;
            
            var methodName = orderedQuery == null ? "OrderBy" : "ThenBy";
            
            var method = typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2).
                    MakeGenericMethod(typeof(TSource), propertyType);
            
            orderedQuery = (IOrderedQueryable<TSource>)method.
                Invoke(null, new object[] { orderedQuery ?? source, orderByExp })!;
        }
        
        return orderedQuery ?? source;
    }
}
