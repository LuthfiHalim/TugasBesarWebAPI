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
    [Route("orderitem")]
    public class OrderItemController : ControllerBase
    {
        private readonly TokoDBContext _context;
        private readonly ILogger<OrderItemController> _logger;
        public OrderItemController(ILogger<OrderItemController> logger, TokoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllOrderItem()
        {
            return Ok(_context.OrderItems);
        }
        [HttpGet("{id}")]
        public IActionResult GetSpecificOrderItem(int id)
        {
            var get = _context.OrderItems.FirstOrDefault(e => e.Id == id);

            if (get == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "User not found" });
            }
            return Ok(get);
        }
        [HttpPost]
        public OrderItem AddOrderItem(OrderItem orderItem)
        {
            var orderItemData = new OrderItem
            {
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity
            };
            _context.OrderItems.Add(orderItemData);
            _context.SaveChanges();
            return orderItem;
        }
        [HttpPut]
        public OrderItem EditOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            _context.SaveChanges();
            return orderItem;
        }
        [HttpDelete]
        public OrderItem DeleteOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
            _context.SaveChanges();
            return orderItem;
        }
    }
}