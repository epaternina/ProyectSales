using Sales.Data;
using Sales.Data.Repositories;
using Sales.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sales.Services
{
    public class CustomerService
    {
        public List<CustomerDto> GetActiveCustomers()
        {
            using (var context = new SalesContext())
            {
                var repo = new CustomerRepository(context);
                return repo.GetActive()
                    .Select(c => new CustomerDto
                    {
                        CustomerId = c.CustomerId,
                        FullName = c.FullName,
                        Email = c.Email
                    }).ToList();
            }
        }

        public int CreateCustomer(CreateCustomerRequest request)
        {
            using (var context = new SalesContext())
            {
                var repo = new CustomerRepository(context);
                return repo.Create(request);
            }
        }
    }
}