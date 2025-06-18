using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using SalesApi.Dto;
// using SalesDetailApi.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using SalesOrderRequestApi.Dto;
// using ProductApi.Dto;
// using SaleCustomerApi.Dto;
using PersonApi.Dto;
using Microsoft.Data.SqlClient;
using server.Models;


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
            var headers = await _context.SalesOrderHeaders.Select(h => new {
                h.SalesOrderID,
                h.RevisionNumber,
                h.OrderDate,
                h.DueDate,
                h.ShipDate,
                h.Status,
                h.SalesOrderNumber,
                h.PurchaseOrderNumber,
                h.AccountNumber,
                h.CustomerID,
                h.SubTotal,
                h.TaxAmt,
                h.Freight,
                h.TotalDue,
                h.Comment,
                h.rowguid,
                h.ModifiedDate
            })
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


        var newSOH = new SalesOrderHeader
            {
                RevisionNumber = salesOrderHeader.SalesOrderHeader.RevisionNumber,
                OrderDate = salesOrderHeader.SalesOrderHeader.OrderDate,
                DueDate = salesOrderHeader.SalesOrderHeader.DueDate,
                ShipDate = salesOrderHeader.SalesOrderHeader.ShipDate,
                Status = salesOrderHeader.SalesOrderHeader.Status,
                OnlineOrderFlag = salesOrderHeader.SalesOrderHeader.OnlineOrderFlag,
                SalesOrderNumber = salesOrderHeader.SalesOrderHeader.SalesOrderNumber,
                PurchaseOrderNumber = salesOrderHeader.SalesOrderHeader.PurchaseOrderNumber,
                AccountNumber = salesOrderHeader.SalesOrderHeader.AccountNumber,
                CustomerID = salesOrderHeader.SalesOrderHeader.CustomerID,
                SalesPersonID = salesOrderHeader.SalesOrderHeader.SalesPersonID,
                TerritoryID = salesOrderHeader.SalesOrderHeader.TerritoryID,
                BillToAddressID = salesOrderHeader.SalesOrderHeader.BillToAddressID,
                ShipToAddressID = salesOrderHeader.SalesOrderHeader.ShipToAddressID,
                ShipMethodID = salesOrderHeader.SalesOrderHeader.ShipMethodID,
                CreditCardID = salesOrderHeader.SalesOrderHeader.CreditCardID,
                CreditCardApprovalCode = salesOrderHeader.SalesOrderHeader.CreditCardApprovalCode,
                CurrencyRateID = salesOrderHeader.SalesOrderHeader.CurrencyRateID,
                SubTotal = salesOrderHeader.SalesOrderHeader.SubTotal,
                TaxAmt = salesOrderHeader.SalesOrderHeader.TaxAmt,
                Freight = salesOrderHeader.SalesOrderHeader.Freight,
                TotalDue = salesOrderHeader.SalesOrderHeader.TotalDue,
                Comment = salesOrderHeader.SalesOrderHeader.Comment,
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.UtcNow
            };

            _context.SalesOrderHeaders.Add(newSOH);
            await _context.SaveChangesAsync();

            foreach (var detail in salesOrderHeader.SalesOrderDetail)
            {
                var newDetail = new SalesOrderDetail
                {
                    SalesOrderID = newSOH.SalesOrderID,
                    CarrierTrackingNumber = detail.CarrierTrackingNumber,
                    OrderQty = detail.OrderQty,
                    ProductID = detail.ProductID,
                    SpecialOfferID = detail.SpecialOfferID,
                    UnitPrice = detail.UnitPrice,
                    UnitPriceDiscount = detail.UnitPriceDiscount,
                    LineTotal = detail.LineTotal, 
                };
                newDetail.ModifiedDate = DateTime.UtcNow;
            
                _context.SalesOrderDetails.Add(newDetail);
            }
       
                await _context.SaveChangesAsync();
                    return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSalesOrder(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Sales Order ID.");
            }

            var SOHToDelete = await _context.SalesOrderHeaders.Where(soh => soh.SalesOrderID == id)
                .FirstOrDefaultAsync();
                if (SOHToDelete == null)
            {
                return NotFound("Sales order not found.");
            }
            _context.SalesOrderHeaders.Remove(SOHToDelete);
            await _context.SaveChangesAsync(); 

            return Ok("Sales order deleted successfully.");
        }


        [HttpGet("details/{id}")]
        public async Task<ActionResult> GetSalesOrdersDetail(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Sales Order ID.");
            }

            var details = await _context.SalesOrderDetails
                    .Join(_context.Products,
                        sod => sod.ProductID,
                        p => p.ProductID,
                        (sod, p)=> new {
                            sod.SalesOrderID,
                            sod.SalesOrderDetailID,
                            sod.CarrierTrackingNumber,
                            sod.OrderQty,
                            sod.ProductID,
                            ProductName = p.Name,
                            sod.SpecialOfferID,
                            sod.UnitPrice,
                            sod.UnitPriceDiscount,
                            sod.LineTotal,
                            sod.ModifiedDate
                        }
                    ).Where(sod => sod.SalesOrderID == id)
                    .ToListAsync();


            return Ok(details);

        }


        [HttpGet("products")]
        public async Task<ActionResult> GetProducts()
        {
          
            var products = await _context.Products
            .Where(p => p.StandardCost > 0 && p.ProductID > 680)
            .Take(100)
            .ToListAsync();

            return Ok(products);

        }

        [HttpGet("customers")]
        public async Task<ActionResult> GetCustomers()
        {


            var customers = await _context.Customers
                        .Join(_context.Persons, 
                            customer => customer.PersonID,
                            person => person.BusinessEntityID,
                            (customer, person) => new{
                                customer.CustomerID,
                                customer.PersonID,
                                Name = person.FirstName,
                                ModifiedDate = customer.ModifiedDate
                            })
                            .Take(100)
                            .ToListAsync();
                            


            return Ok(customers);

        }


    
    
     }
}