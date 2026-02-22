using Sales.Models;
using Sales.Services;
using System;
using System.Web.Http;

namespace ProyectSales.Controllers
{
    [RoutePrefix("api/sales")]
    public class SalesController : ApiController
    {

        private readonly SaleService _service;

        // Constructor con inyección
        public SalesController(SaleService service)
        {
            _service = service;
        }

        // Constructor por defecto (requerido por Web API 2)
        public SalesController() : this(new SaleService()) { }

        [HttpPost]
        public IHttpActionResult Create([FromBody] CreateSaleRequest request)
        {
            if (request == null)
                return BadRequest("El cuerpo de la solicitud es requerido.");

            if (request.CustomerId <= 0)
                return BadRequest("CustomerId es requerido.");

            if (request.Items == null || request.Items.Count == 0)
                return BadRequest("La venta debe tener al menos un producto.");

            try
            {
                int saleId = _service.CreateSale(request);
                return Ok(new { SaleId = saleId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                SaleDetailResponse sale = _service.GetById(id);
                if (sale == null)
                    return NotFound();

                return Ok(sale);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}