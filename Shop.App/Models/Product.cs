using System;
using System.Collections.Generic;

namespace Shop.App.Models
{
    public partial class Product:BaseModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
