using HelloWorldWebAPI.Context;
using HelloWorldWebAPI.Models;
using HelloWorldWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApplication.Test.MockData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProductApplication.Test
{
    public class ProductTest
    {
        IProductService _service;
        public static DbContextOptions<ProductContext> options { get; }
        public static string connectionString = "";

        static ProductTest()
        {
            options = new DbContextOptionsBuilder<ProductContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        public ProductTest()
        {
            var _context = new ProductContext(options);
            MockDataSeed data = new MockDataSeed();
            data.Seed(_context);
            _service = new ProductService(_context);
        }

        [Fact]
        public async void GetAll_Success()
        {
            //Arrange

            //Act
            var result = await _service.GetAll();

            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async void GetById_Success()
        {
            //Arrange
            int valid = 2;
            
            //Act
             var resultValid =await _service.GetProductById(valid);

            //Assert
            Assert.NotNull(resultValid);
            Assert.IsType<Product>(resultValid);



        }

        [Fact]
        public async void Add_Success()
        {
            //Arrange
            Product testProduct = new Product()
            {
                
                Name = "TestProduct3",
                Description = "Test Product3",
                Price = 3
            };

            //Act
            var result = await _service.AddProduct(testProduct);          

            //Assert
            
            Assert.IsType<Product>(result);
            Assert.Equal("TestProduct3", result.Name);
            Assert.Equal("Test Product3", result.Description);
            Assert.Equal(3, result.Price);

        }

        [Fact]
        public async void Update_Success()
        {
            //Arrange
            var productId = 1;

            //Act
            var currentProduct = await _service.GetProductById(productId);

            Product newProduct = new Product()
            {
                ProductID = currentProduct.ProductID,
                Name = "UpdatedTestProduct",
                Description = "Updated Test Product",
                Price = currentProduct.Price
            };

            var result = await _service.UpdateProduct(newProduct);

            //Assert

            Assert.IsType<Product>(result);
            Assert.Equal("UpdatedTestProduct", result.Name);
            Assert.Equal("Updated Test Product", result.Description);
            Assert.Equal(1, result.Price);

        }

        [Fact]
        public async void Delete_Success()
        {
            //Arrange
            var productId = 1;

            //Act
            //var product = await _service.GetProductById(productId);

            var result = await _service.DeleteProduct(productId);

            //Assert

            Assert.IsType<Product>(result);

        }
    }
}
