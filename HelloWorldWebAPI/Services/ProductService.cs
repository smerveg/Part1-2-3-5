using HelloWorldWebAPI.Context;
using HelloWorldWebAPI.DTOs;
using HelloWorldWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductContext _context;
        public ProductService(ProductContext context)
        {
            _context = context;
        }
        public async Task<Product> AddProduct(Product product)
        {
            var entity = _context.Entry(product);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(int productId)
        {
            var product = _context.Products.Find(productId);
            var entity = _context.Entry(product);
            entity.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var result=await _context.Products.ToListAsync();
            return result;
        }

        public async Task<Product> GetProductById(int productId)
        {
            var result = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductID == productId);
            return result;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var entity = _context.Entry(product);
            entity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
