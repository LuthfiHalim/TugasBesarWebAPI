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
    [Route("deliveryaddress")]
    public class OrderController : ControllerBase
    {
        private readonly TokoDBContext _context;
        private readonly ILogger<OrderController> _logger;
        public OrderController(ILogger<OrderController> logger, TokoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllOrder()
        {
            return Ok(_context.Orders);
        }
        [HttpGet("{id}")]
        public IActionResult GetSpecificOrder(int id)
        {
            var get = _context.Orders.FirstOrDefault(e => e.Id == id);

            if (get == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "User not found" });
            }
            return Ok(get);
        }
        [HttpPost]
        public Order AddOrder(Order order)
        {
            var orderData = new Order
            {
                CustomerId = order.CustomerId,
                DeliveryAddressId = order.DeliveryAddressId,
                OrderItems = order.OrderItems
            };
            _context.Orders.Add(orderData);
            _context.SaveChanges();
            return order;
        }
        [HttpPut]
        public Order EditOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
            return order;
        }
        [HttpDelete]
        public Order DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return order;
        }
    }
}