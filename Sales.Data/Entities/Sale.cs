using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Data.Entities
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
        public DateTime SaleDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; }
    }
}
