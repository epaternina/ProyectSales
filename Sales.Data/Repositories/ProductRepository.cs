using Sales.Data.Entities;
using Sales.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sales.Data.Repositories
{
    public  class ProductRepository
    {
        private readonly SalesContext _context;

        public ProductRepository(SalesContext context)
        {
            _context = context;
        }

        public List<Product> GetActive()
        {
            return _context.Products
                .Where(p => p.IsActive)
                .ToList();
        }

        public int Create(CreateProductRequest request)
        {
            var product = new Entities.Product
            {
                Name = request.Name,
                Price = request.Price,
                IsActive = true,
                CreatedAt = System.DateTime.Now
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.ProductId;
        }
    }
}
