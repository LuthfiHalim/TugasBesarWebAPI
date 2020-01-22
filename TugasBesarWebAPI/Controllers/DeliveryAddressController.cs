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
    public class DeliveryAddressController : ControllerBase
    {
        private readonly TokoDBContext _context;
        private readonly ILogger<DeliveryAddressController> _logger;
        public DeliveryAddressController(ILogger<DeliveryAddressController> logger, TokoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllDeliveryAddress()
        {
            return Ok(_context.DeliveryAddresss);
        }
        [HttpGet("{id}")]
        public IActionResult GetSpecificDeliveryAddress(int id)
        {
            var get = _context.DeliveryAddresss.FirstOrDefault(e => e.Id == id);

            if (get == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "User not found" });
            }
            return Ok(get);
        }
        [HttpPost]
        public DeliveryAddress AddDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            var deliveryAddressData = new DeliveryAddress
            {
                Name = deliveryAddress.Name,
                Address = deliveryAddress.Address,
                Orders = deliveryAddress.Orders
            };
            _context.DeliveryAddresss.Add(deliveryAddressData);
            _context.SaveChanges();
            return deliveryAddress;
        }
        [HttpPut]
        public DeliveryAddress EditDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            _context.DeliveryAddresss.Update(deliveryAddress);
            _context.SaveChanges();
            return deliveryAddress;
        }
        [HttpDelete]
        public DeliveryAddress DeleteDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            _context.DeliveryAddresss.Remove(deliveryAddress);
            _context.SaveChanges();
            return deliveryAddress;
        }
    }
}