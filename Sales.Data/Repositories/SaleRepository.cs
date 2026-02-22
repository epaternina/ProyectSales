using Sales.Data.Entities;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sales.Data.Repositories
{
    public class SaleRepository
    {
        private readonly SalesContext _context;
        private readonly string _connectionString;

        public SaleRepository(SalesContext context)
        {
            _context = context;
            _connectionString = ConfigurationManager.ConnectionStrings["SalesDB"].ConnectionString;
        }

        // ADO.NET + Stored Procedure
        public int CreateSale(int customerId, List<SaleItemRequest> items)
        {
            // Armar XML de items
            var sb = new StringBuilder("<Items>");
            foreach (var item in items)
                sb.Append($"<Item ProductId=\"{item.ProductId}\" Quantity=\"{item.Quantity}\"/>");
            sb.Append("</Items>");

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("dbo.sp_CreateSale", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Parameters.AddWithValue("@SaleDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ItemsXml", sb.ToString());

                    var outputParam = new SqlParameter("@NewSaleId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    return (int)outputParam.Value;
                }
            }
        }

        public Sale GetById(int saleId)
        {
            return _context.Sales
                .Include("Customer")
                .Include("SaleItems.Product")
                .FirstOrDefault(s => s.SaleId == saleId);
        }
    }
}
