using HelloWorldWebAPI.DTOs;
using HelloWorldWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetProductById(int productId);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(int productId);
    }
}
