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
            doSorting(ref query,param, pagination.Sortings);
            doFilterNumbers(ref query, param, pagination.FilterNums);
            doFilterStrings(ref query, param, pagination.FilterStrings);
            return await query.Skip(pagination.PageIdx * pagination.PageSize).Take(pagination.PageSize)
            .ToListAsync();
        }

        private void doFilterStrings<T>(ref IQueryable<T> query, ParameterExpression param, List<keyPairValueString>? filterStrings) where T : class
        {
            foreach (var item in filterStrings)
            {
                var filterByPropertyExpression = Expression.Property(param, item.Name);
                var filterValueExpression = Expression.Constant(item.Value);
                var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                Expression comparison = Expression.Call(filterByPropertyExpression, method, filterValueExpression); ;
                var whereExpression = Expression.Lambda<Func<T, bool>>(comparison, param);
                query = query.Where(whereExpression);
            }
        }

        private void doFilterNumbers<T>(ref IQueryable<T> query, ParameterExpression param, List<keyPairValueNums>? filterNums) where T : class
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
                query = query.Where(whereExpression);
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
