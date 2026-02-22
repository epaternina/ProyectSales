using Sales.Models;
using Sales.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ProyectSales.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly ProductService _service;

        // Constructor con inyección
        public ProductsController(ProductService service)
        {
            _service = service;
        }

        // Constructor por defecto (requerido por Web API 2)
        public ProductsController() : this(new ProductService()) { }

        [HttpGet]
        public IHttpActionResult Get()
        {
            List<ProductDto> products = _service.GetActiveProducts();
            return Ok(products);
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] CreateProductRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name) || request.Price <= 0)
                return BadRequest("Nombre y precio válido son requeridos.");

            try
            {
                var service = new ProductService();
                int id = service.CreateProduct(request);
                return Ok(new { ProductId = id });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}