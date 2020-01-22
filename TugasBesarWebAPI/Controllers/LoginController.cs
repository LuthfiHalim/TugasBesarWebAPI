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
    [AllowAnonymous]
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly TokoDBContext _context;
        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger, TokoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpPost]
        public IActionResult Authenticate(Login login)
        {
            var _login = _context.Logins.SingleOrDefault(e => e.Username == login.Username);
            if (_login == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, _login.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("berlian sistem informasi")), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userNew = new
            {
                username = _login.Username,
                id = _login.Id,
                password = _login.Password,
                token = tokenHandler.WriteToken(token)
            };
            return Ok(userNew);
        }
    }
}