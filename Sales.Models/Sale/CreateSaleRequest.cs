using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Models
{
    public class CreateSaleRequest
    {
        public int CustomerId { get; set; }
        public List<SaleItemRequest> Items { get; set; }
    }
}
