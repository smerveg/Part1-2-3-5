using HelloWorldWebAPI.Context;
using HelloWorldWebAPI.DTOs;
using HelloWorldWebAPI.Models;
using HelloWorldWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductService.Test
{
    public class HelloWorldTest
    {
         IProductService _service;
        ProductContext _context;


        public HelloWorldTest()
        {
            _service = new ProductService(_context);
        }

        [Fact]
        public void GetAll_Success()
        {
            //Arrange

            //Act
            var result = _service.GetAll();
            var resultType=result.Result as OkObjectResult;
            var list = resultType.Value as List<Product>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<Product>>(resultType.Value);

        }
    }
}
