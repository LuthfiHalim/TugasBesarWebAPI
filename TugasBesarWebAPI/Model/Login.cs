using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TugasBesarWebAPI.Model
{
    public class Login
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
