using HelloWorldWebAPI.Context;
using HelloWorldWebAPI.DTOs;
using HelloWorldWebAPI.Exceptions;
using HelloWorldWebAPI.Models;
using HelloWorldWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebAPI.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName ="ProductCache")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var result = await _service.GetAll();

            if (result == null)
            {
                throw new NotFoundException("The product list could not found.");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("{productId}")]
        [ResponseCache(CacheProfileName = "ProductCache")]
        public async Task<ActionResult<Product>> GetById(int productId)
        {
            var result = await _service.GetProductById(productId);

            if (result == null)
            {
                throw new NotFoundException($"The record with ID number {productId} could not found.");
            }
            else
            {
                return Ok(result);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Add([FromQuery]Product product)
        {
            if (product!=null)
            {
                await _service.AddProduct(product);
                return Ok();
            }
            else
            {
                throw new BadRequestException("The product could not added.");
            }


        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Product>> Update([FromQuery] Product product)
        {
            var curentProduct = await _service.GetProductById(product.ProductID);

            if (curentProduct == null)
            {
                throw new NotFoundException("The record could not found.");
            }
            else
            {
                await _service.UpdateProduct(product);
                return Ok(product);

            }
        }

        [Authorize]
        [HttpDelete("{productId}")]
        public async Task<ActionResult> Delete(int productId)
        {
            var product = await _service.GetProductById(productId);

            if (product == null)
            {
                throw new NotFoundException("The record could not found.");
            }
            else
            {
                await _service.DeleteProduct(productId);
                return Ok();

            }
        }
    }
}
