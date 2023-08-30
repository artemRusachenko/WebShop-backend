using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace lesson1_Simple_Functions___Controller.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = "";
        public int ParentId { get; set; }
        [NotMapped]
        public List<Product>? Products { get; set; } = new List<Product>();
    }
}
