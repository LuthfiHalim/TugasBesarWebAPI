using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TugasBesarWebAPI.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("CategoryId")]
        public List<Product> Products { get; set; }
    }
}
