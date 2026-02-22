using Sales.Data.Entities;
using Sales.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sales.Data.Repositories
{
    public class CustomerRepository
    {
        private readonly SalesContext _context;

        public CustomerRepository(SalesContext context)
        {
            _context = context;
        }

        public List<Customer> GetActive()
        {
            return _context.Customers
                .Where(c => c.IsActive)
                .ToList();
        }

        public int Create(CreateCustomerRequest request)
        {
            var customer = new Entities.Customer
            {
                FullName = request.FullName,
                Email = request.Email,
                IsActive = true,
                CreatedAt = System.DateTime.Now
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer.CustomerId;
        }
    }
}
