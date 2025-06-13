using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesApi.Dto;
using SalesDetailApi.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using SalesOrderRequestApi.Dto;
using ProductApi.Dto;

namespace SalesApi.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class ServicesControllers : ControllerBase
    {
        private readonly ServerDbContext _context;
        public ServicesControllers(ServerDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetSalesOrders()
        {
            string sqlQuery = @"
                SELECT TOP 100
                SalesOrderId,
                RevisionNumber,
                OrderDate,
                DueDate,
                ShipDate,
                Status,
                SalesOrderNumber,
                PurchaseOrderNumber,
                AccountNumber,
                
                SubTotal,
                TaxAmt,
                Freight,
                TotalDue,
                Comment,
                rowguid,
                ModifiedDate
                FROM [AdventureWorks2016].[Sales].[SalesOrderHeader]";



            var headers = await _context.Set<SalesOrderHeader>()
                                        .FromSqlRaw(sqlQuery)
                                        .AsNoTracking()    
                                        .ToListAsync();

            return Ok(headers);

        }

        [HttpPost]
        public async Task<ActionResult> CreateSalesOrder(SalesOrderRequest salesOrderHeader)
        {
            if (salesOrderHeader == null)
            {
                return BadRequest("Sales order header cannot be null.");
            }
            Console.WriteLine(salesOrderHeader.SalesOrderHeader);
            Console.WriteLine(salesOrderHeader.SalesOrderDetail);
            

            // _context.SalesOrderHeaders.Add(salesOrderHeader);
            // await _context.SaveChangesAsync();

            // return CreatedAtAction(nameof(GetSalesOrders), new { id = salesOrderHeader.SalesOrderId }, salesOrderHeader);
            return Ok();
        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult> GetSalesOrdersDetail(int id)
        {
            string sqlQuery = $@"
                SELECT TOP 100
                    SOD.SalesOrderId,
                    SOD.SalesOrderDetailId,
                    SOD.CarrierTrackingNumber,
                    SOD.OrderQty,
                    P.Name AS ProductName,
                    SOD.UnitPrice,
                    SOD.UnitPriceDiscount,
                    SOD.LineTotal,
                    SOD.ModifiedDate
                FROM [AdventureWorks2016].[Sales].[SalesOrderDetail] AS SOD
                JOIN [AdventureWorks2016].[Production].[Product] AS P
                    ON SOD.ProductID = P.ProductID
                WHERE SOD.SalesOrderId = {id}
            ";




            var headers = await _context.Set<SalesOrderDetail>()
                                        .FromSqlRaw(sqlQuery)
                                        .AsNoTracking()    
                                        .ToListAsync();

            return Ok(headers);

        }


        [HttpGet("products")]
        public async Task<ActionResult> GetProducts()
        {
            string sqlQuery = $@"
                SELECT TOP 100
                    *
                FROM [AdventureWorks2016].[Production].[Product]
            ";




            var products = await _context.Set<Product>()
                                        .FromSqlRaw(sqlQuery)
                                        .AsNoTracking()    
                                        .ToListAsync();

            return Ok(products);

        }
    
    
    }
}