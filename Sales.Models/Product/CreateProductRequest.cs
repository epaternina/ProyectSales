using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Models
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
