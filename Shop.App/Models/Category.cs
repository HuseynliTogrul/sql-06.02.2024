using System;
using System.Collections.Generic;

namespace Shop.App.Models
{
    public partial class Category:BaseModel
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
