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
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private readonly TokoDBContext _context;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ILogger<CategoryController> logger, TokoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            return Ok(_context.Categorys);
        }
        [HttpGet("{id}")]
        public IActionResult GetSpecificCategory(int id)
        {
            var get = _context.Categorys.FirstOrDefault(e => e.Id == id);

            if (get == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "User not found" });
            }
            return Ok(get);
        }
        [HttpPost]
        public Category AddCategory(Category category)
        {
            var categoryData = new Category
            {
                Name = category.Name,
                Products = category.Products
            };
            _context.Categorys.Add(categoryData);
            _context.SaveChanges();
            return category;
        }
        [HttpPut]
        public Category EditCategory(Category category)
        {
            _context.Categorys.Update(category);
            _context.SaveChanges();
            return category;
        }
        [HttpDelete]
        public Category DeleteCategory(Category category)
        {
            _context.Categorys.Remove(category);
            _context.SaveChanges();
            return category;
        }
    }
}