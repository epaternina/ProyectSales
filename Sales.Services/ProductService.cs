using Sales.Data;
using Sales.Data.Repositories;
using Sales.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sales.Services
{
    public class ProductService
    {
        public List<ProductDto> GetActiveProducts()
        {
            using (var context = new SalesContext())
            {
                var repo = new ProductRepository(context);
                return repo.GetActive()
                    .Select(p => new ProductDto
                    {
                        ProductId = p.ProductId,
                        Name = p.Name,
                        Price = p.Price
                    }).ToList();
            }
        }

        public int CreateProduct(CreateProductRequest request)
        {
            using (var context = new SalesContext())
            {
                var repo = new ProductRepository(context);
                return repo.Create(request);
            }
        }
    }
}