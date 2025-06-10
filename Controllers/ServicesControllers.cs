using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesApi.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Server.Data;

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
    }
}