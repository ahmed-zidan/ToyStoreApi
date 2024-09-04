using Abp.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;
using ToyStore.Core.SharedModels;
using ToyStore.Infrastructure.Data;

namespace ToyStore.Infrastructure.Repo
{
    public class PaginationRepo : IPaginationRepo
    {
        private readonly AppDbContext _context;
        public PaginationRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetDataByDynamicPropertyAsync<T>(GenericPagination pagination) where T : class
        {
            var param = Expression.Parameter(typeof(T), "x");
            var dbSet = _context.Set<T>();
            var query = dbSet.AsQueryable();
            Expression<Func<T, bool>> predicate = pagination.IsAndOperator? PredicateBuilder.True<T>() : PredicateBuilder.False<T>() ;
            doFilterNumbers(ref predicate, param, pagination.FilterNums,pagination.IsAndOperator);
            doFilterStrings(ref predicate, param, pagination.FilterStrings, pagination.IsAndOperator);
            
            query = query.Where(predicate);

            doSorting(ref query, param, pagination.Sortings);
            return await query.Skip(pagination.PageIdx * pagination.PageSize).Take(pagination.PageSize)
            .ToListAsync();
        }

        private void doFilterStrings<T>(ref Expression<Func<T, bool>> predicate, ParameterExpression param, List<keyPairValueString>? filterStrings,
            bool isAnd) where T : class
        {
            foreach (var item in filterStrings)
            {
                var filterByPropertyExpression = Expression.Property(param, item.Name);
                var filterValueExpression = Expression.Constant(item.Value);
                var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                Expression comparison = Expression.Call(filterByPropertyExpression, method, filterValueExpression); ;
                var whereExpression = Expression.Lambda<Func<T, bool>>(comparison, param);
                if(isAnd)
                    predicate = predicate.And(whereExpression);
                else predicate = predicate.Or(whereExpression);
                //query = query.Where(whereExpression);
            }
        }

        private void doFilterNumbers<T>(ref Expression<Func<T, bool>> predicate, ParameterExpression param, List<keyPairValueNums>? filterNums,
            bool isAnd) where T : class
        {
            foreach (var item in filterNums)
            {
                var filterByPropertyExpression = Expression.Property(param, item.Name);
                var filterValueExpression = Expression.Constant(item.Value);
                Expression comparison = null;
                switch (item.Operator)
                {
                    case ">":
                        {
                            comparison = Expression.GreaterThan(filterByPropertyExpression, filterValueExpression);
                            break;
                        }
                    case "<":
                        {
                            comparison = Expression.LessThan(filterByPropertyExpression, filterValueExpression);
                            break;
                        }
                    case ">=":
                        {
                            comparison = Expression.GreaterThanOrEqual(filterByPropertyExpression, filterValueExpression);
                            break;
                        }
                    case "<=":
                        {
                            comparison = Expression.LessThanOrEqual(filterByPropertyExpression, filterValueExpression);
                            break;
                        }
                    case "!=":
                        {
                            comparison = Expression.NotEqual(filterByPropertyExpression, filterValueExpression);
                            break;
                        }
                    default:
                        {
                            comparison = Expression.Equal(filterByPropertyExpression, filterValueExpression);
                            break;
                        }
                }
                var whereExpression = Expression.Lambda<Func<T, bool>>(comparison, param);
                if (isAnd)
                    predicate = predicate.And(whereExpression);
                else predicate = predicate.Or(whereExpression);
                //query = query.Where(whereExpression);
            }
        }

        private void doSorting<T>(ref IQueryable<T> query,ParameterExpression param, List<keyPairValueString>? sortings) where T : class
        {
            //dynamic sorting
            foreach (var item in sortings)
            {
                var orderByPropertyExpression = Expression.Property(param, item.Name);
                var orderByExpression = Expression.Lambda<Func<T, object>>(
                Expression.Convert(orderByPropertyExpression, typeof(object)), param);
                if ("ascending".Contains(item.Value))
                {
                    query = query.OrderBy(orderByExpression);
                }
                else
                {
                    query = query.OrderBy(orderByExpression);
                }
            }
        }
    }
}
