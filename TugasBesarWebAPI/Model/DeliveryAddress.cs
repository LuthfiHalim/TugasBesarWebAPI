using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TugasBesarWebAPI.Model
{
    public class DeliveryAddress
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [ForeignKey("DeliveryAddressId")]
        public List<Order> Orders { get; set; }
    }
}
