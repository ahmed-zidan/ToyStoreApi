using Abp.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;
using ToyStore.Core.SharedModels;
using ToyStore.Infrastructure.Data;

namespace ToyStore.Infrastructure.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product model)
        {
            await _context.Products.AddAsync(model);
        }

        public void Delete(Product model)
        {
            _context.Products.Remove(model);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(x=>x.Category).Include(x=>x.colors).Include(x=>x.Sizes).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> GetProductByNameAsync(string Name)
        {
            return await _context.Products.Include(x=>x.Category).FirstOrDefaultAsync(x => x.Name == Name);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(PaginationDto pagination)
        {
          
            var products = _context.Products.Include(x=>x.Category).AsQueryable();
            filterByCatgory(ref products, pagination.CategoryId);
            filterByPrice(ref products, pagination.MinPrice,pagination.MaxPrice);
            filterBySearch(ref products, pagination.Search);
            filterBySizes(ref products, pagination.Sizes);
            filterByColors(ref products, pagination.Colors);
           if (pagination.Sorting != null) {

                products = pagination.Sorting switch
                {
                    "PriceAsc" => products.OrderBy(x => x.SellPrice),
                    "PriceDesc"=>products.OrderByDescending(x=>x.SellPrice),
                    _=>products.OrderBy(x=>x.Name)
                };
            }
            return await products.Skip(pagination.PageIdx * pagination.PageSize).Take(pagination.PageSize).Include(x=>x.Sizes).Include(x=>x.colors).ToListAsync();
        }

        private void filterByPrice(ref IQueryable<Product> products, int minPrice, int maxPrice)
        {
            products = products.Where(x => x.SellPrice >= minPrice && x.SellPrice <= maxPrice);
        }

        public async Task<int> productCount(PaginationDto pagination)
        {
            var products = _context.Products.AsQueryable();
            filterByCatgory(ref products, pagination.CategoryId);
            filterByPrice(ref products, pagination.MinPrice, pagination.MaxPrice);
            filterBySearch(ref products, pagination.Search);
            filterBySizes(ref products, pagination.Sizes);
            filterByColors(ref products, pagination.Colors);
            
            return await products.CountAsync();
        }

        private void filterByCatgory(ref IQueryable<Product> products , int? categoryId)
        {
            if (categoryId != null)
            {
                products = products.Where(x => x.CategotyId == categoryId);
            }
            
        }

        private void filterBySearch(ref IQueryable<Product> products, string? Search)
        {
            if (Search != null)
            {
                products = products.Where(x => x.Name.ToLower().Contains(Search.ToLower()));
            }
        }

        private void filterBySizes(ref IQueryable<Product> products, List<int>? Sizes)
        {
            if (Sizes != null && Sizes.Count > 0)
            {
                Expression<Func<Product, bool>> predicate = PredicateBuilder.False<Product>();
                foreach (var item in Sizes)
                {
                    predicate = predicate.Or(x => x.Sizes.Any(y => y.Id == item));
                }
                products = products.Where(predicate);
            }
        }
        private void filterByColors(ref IQueryable<Product> products, List<int>? Colors)
        {
            if (Colors != null && Colors.Count > 0)
            {
                Expression<Func<Product, bool>> predicate = PredicateBuilder.False<Product>();
                foreach (var item in Colors)
                {
                    predicate = predicate.Or(x => x.colors.Any(y => y.Id == item));
                }
                products = products.Where(predicate);
            }
        }

        public void Update(Product model)
        {
            _context.Products.Update(model);
        }
    }
}
