using Sales.Data;
using Sales.Data.Repositories;
using Sales.Models;
using System;
using System.Linq;

namespace Sales.Services
{
    public class SaleService
    { 
        public int CreateSale(CreateSaleRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request.Items == null || request.Items.Count == 0)
                throw new ArgumentException("La venta debe tener al menos un producto.");

            foreach (var item in request.Items)
            {
                if (item.Quantity <= 0)
                    throw new ArgumentException(
                        $"La cantidad del producto {item.ProductId} debe ser mayor a 0.");
            }

            using (var context = new SalesContext())
            {
                var repo = new SaleRepository(context);
                return repo.CreateSale(request.CustomerId, request.Items);
            }
        }

        public SaleDetailResponse GetById(int saleId)
        {
            using (var context = new SalesContext())
            {
                var repo = new SaleRepository(context);
                var sale = repo.GetById(saleId);

                if (sale == null)
                    return null;

                return new SaleDetailResponse
                {
                    SaleId = sale.SaleId,
                    CustomerName = sale.Customer.FullName,
                    Total = sale.Total,
                    SaleDate = sale.SaleDate,
                    Items = sale.SaleItems.Select(i => new SaleItemResponse
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Product.Name,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        Subtotal = i.Subtotal
                    }).ToList()
                };
            }
        }
    }
}