using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Models
{
    public class SaleDetailResponse
    {
        public int SaleId { get; set; }
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
        public DateTime SaleDate { get; set; }
        public List<SaleItemResponse> Items { get; set; }
    }
}
