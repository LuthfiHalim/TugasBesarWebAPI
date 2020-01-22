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
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly TokoDBContext _context;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ILogger<CustomerController> logger, TokoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            return Ok(_context.Customers.Include("Orders"));
        }
        [HttpGet("{id}")]
        public IActionResult GetSpecificCustomer(int id)
        {
            var user = _context.Customers.FirstOrDefault(e => e.Id == id);

            if (user == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "User not found" });
            }
            return Ok(user);
        }
        [HttpPost]
        public Customer AddCustomer(Customer customer)
        {
            var customerData = new Customer
            {
                Address = customer.Address,
                Logins = customer.Logins,
                Name = customer.Name,
            };
            _context.Customers.Add(customerData);
            _context.SaveChanges();
            return customerData;
        }
        [HttpPut]
        public Customer EditCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return customer;
        }
        [HttpDelete]
        public Customer DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return customer;
        }
    }
}