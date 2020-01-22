using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TugasBesarWebAPI.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int DeliveryAddressId { get; set; }
        [ForeignKey("OrderId")]
        public List<OrderItem> OrderItems { get; set; }
    }
}
