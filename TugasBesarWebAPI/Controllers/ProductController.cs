using System.Net;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TugasBesarWebAPI.Model;
using TugasBesarWebAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
namespace core_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly TokoDBContext _context;
        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger, TokoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            return Ok(_context.Products);
        }
        [HttpGet("{id}")]
        public IActionResult GetSpecificProduct(int id)
        {
            var get = _context.Products.FirstOrDefault(e => e.Id == id);

            if (get == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "User not found" });
            }
            return Ok(get);
        }
        [HttpPost]
        public Product AddProduct(Product product)
        {
            var productData = new Product
            {
                CategoryId = product.CategoryId,
                Name = product.Name,
                OrderItems = product.OrderItems,
                Price = product.Price
            };
            _context.Products.Add(productData);
            _context.SaveChanges();
            return product;
        }
        [HttpPut]
        public Product EditProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return product;
        }
        [HttpDelete]
        public Product DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return product;
        }
    }
}