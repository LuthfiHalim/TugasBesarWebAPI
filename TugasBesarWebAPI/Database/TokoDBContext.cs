using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TugasBesarWebAPI.Model;


namespace TugasBesarWebAPI.Database
{
    public class TokoDBContext : DbContext
    {
        public TokoDBContext(DbContextOptions<TokoDBContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresss { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
