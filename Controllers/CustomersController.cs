using Sales.Models;
using Sales.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ProyectSales.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private readonly CustomerService _service;

        // Constructor con inyección
        public CustomersController(CustomerService service)
        {
            _service = service;
        }

        // Constructor por defecto (requerido por Web API 2)
        public CustomersController() : this(new CustomerService()) { }


        [HttpGet]
        public IHttpActionResult Get()
        {
            List<CustomerDto> customers = _service.GetActiveCustomers();
            return Ok(customers);
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] CreateCustomerRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.FullName) || string.IsNullOrEmpty(request.Email))
                return BadRequest("Nombre y email son requeridos.");

            try
            {
                var service = new CustomerService();
                int id = service.CreateCustomer(request);
                return Ok(new { CustomerId = id });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}