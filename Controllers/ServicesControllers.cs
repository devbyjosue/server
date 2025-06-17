using System;
using System.Data;
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
using SaleCustomerApi.Dto;
using PersonApi.Dto;
using Microsoft.Data.SqlClient;


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
                SELECT 
                SalesOrderId,
                RevisionNumber,
                OrderDate,
                DueDate,
                ShipDate,
                Status,
                SalesOrderNumber,
                PurchaseOrderNumber,
                AccountNumber,
                CustomerID,
                
                SubTotal,
                TaxAmt,
                Freight,
                TotalDue,
                Comment,
                rowguid,
                ModifiedDate
                FROM [AdventureWorks2016Test].[Sales].[SalesOrderHeader]";



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
            var insertedIdParameter = new SqlParameter
                {
                    ParameterName = "@InsertedId", 
                    SqlDbType = SqlDbType.Int, 
                    Direction = ParameterDirection.Output
                };

                    string insertHeaderQuery = @"
                    INSERT INTO Sales.SalesOrderHeader (
                        RevisionNumber, OrderDate, DueDate, ShipDate, Status, OnlineOrderFlag, 
                        PurchaseOrderNumber, AccountNumber, CustomerID, SalesPersonID, 
                        TerritoryID, BillToAddressID, ShipToAddressID, ShipMethodID, CreditCardID, 
                        CreditCardApprovalCode, CurrencyRateID, SubTotal, TaxAmt, Freight, Comment, rowguid, ModifiedDate
                    )
                    VALUES (
                        1, @OrderDate, @DueDate, NULL, 1, 1, 
                        NULL, 'ACC12345', @CustomerID, 282, 5, @BillToAddressID, 5, 5, 5, 
                        5, 5, 50000.00, 1567.82, 90.00, 'Test Order', NEWID(), @ModifiedDate
                    );
                    SET @InsertedId = SCOPE_IDENTITY(); 
                ";

          await _context.Database.ExecuteSqlRawAsync(insertHeaderQuery,
                new SqlParameter("@OrderDate", salesOrderHeader.SalesOrderHeader.OrderDate),
                new SqlParameter("@DueDate", salesOrderHeader.SalesOrderHeader.DueDate),
                new SqlParameter("@CustomerID", salesOrderHeader.SalesOrderHeader.CustomerID),
                new SqlParameter("@BillToAddressID", 529),
                new SqlParameter("@ModifiedDate", salesOrderHeader.SalesOrderHeader.ModifiedDate),
                insertedIdParameter
            );
            int lastInsertedId = Convert.ToInt32(insertedIdParameter.Value); 

            // var insertedId = await _context.SalesOrderHeaders
            //         .OrderByDescending(x => x.SalesOrderID)
            //         .Select(x => x.SalesOrderID)
            //         .FirstOrDefaultAsync();


            Console.WriteLine($"Inserted SalesOrderID --------------------------------------------------------------------------: {lastInsertedId}");



            foreach (var detail in salesOrderHeader.SalesOrderDetail)
                    {
                        string insertDetailQuery = @"
                            INSERT INTO Sales.SalesOrderDetail (SalesOrderID, CarrierTrackingNumber, OrderQty, ProductID, SpecialOfferID, UnitPrice, UnitPriceDiscount, ModifiedDate)
                            VALUES (@SalesOrderID, @CarrierTrackingNumber, @OrderQty, @ProductID, @SpecialOfferID, @UnitPrice, @UnitPriceDiscount, @ModifiedDate);
                        ";

                        await _context.Database.ExecuteSqlRawAsync(insertDetailQuery,
                            new SqlParameter("@SalesOrderID", lastInsertedId),
                            new SqlParameter("@CarrierTrackingNumber", detail.CarrierTrackingNumber),
                            new SqlParameter("@OrderQty", detail.OrderQty),
                            new SqlParameter("@ProductID", detail.ProductId),
                            new SqlParameter("@SpecialOfferID", detail.SpecialOfferId ?? 1),
                            new SqlParameter("@UnitPrice", detail.UnitPrice),
                            new SqlParameter("@UnitPriceDiscount", detail.UnitPriceDiscount),
                            new SqlParameter("@LineTotal", detail.LineTotal),
                            new SqlParameter("@ModifiedDate", detail.ModifiedDate)
                        );
                    }

                    return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSalesOrder(int id)
        {
            string deleteHeaderQuery = @"
                DELETE FROM Sales.SalesOrderHeader
                WHERE SalesOrderID = @SalesOrderID;
            ";

            await _context.Database.ExecuteSqlRawAsync(deleteHeaderQuery,
                new SqlParameter("@SalesOrderID", id)
            );

            return Ok("Sales order deleted successfully.");
        }


        [HttpGet("details/{id}")]
        public async Task<ActionResult> GetSalesOrdersDetail(int id)
        {
            string sqlQuery = $@"
                SELECT 
                    SOD.SalesOrderId,
                    SOD.SalesOrderDetailId,
                    SOD.CarrierTrackingNumber,
                    SOD.OrderQty,
                    SOD.ProductID,
                    P.Name AS ProductName,
                    SOD.SpecialOfferId,
                    SOD.UnitPrice,
                    SOD.UnitPriceDiscount,
                    SOD.LineTotal,
                    SOD.ModifiedDate
                FROM [AdventureWorks2016Test].[Sales].[SalesOrderDetail] AS SOD
                JOIN [AdventureWorks2016Test].[Production].[Product] AS P
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
                SELECT TOP 10
                    *
                FROM [AdventureWorks2016].[Production].[Product]
                WHERE standardCost > 0 AND ProductID > 680;
            ";




            var products = await _context.Set<Product>()
                                        .FromSqlRaw(sqlQuery)
                                        .AsNoTracking()    
                                        .ToListAsync();

            return Ok(products);

        }

        [HttpGet("customers")]
        public async Task<ActionResult> GetCustomers()
        {


            string sqlQuery = $@"
                SELECT TOP 100
                    SC.CustomerID,
                    SC.PersonID,
                    P.FirstName AS Name,
                    SC.ModifiedDate
                FROM [AdventureWorks2016].[Sales].[Customer] AS SC
                JOIN [AdventureWorks2016].[Person].[Person] AS P
                    ON SC.PersonID = P.BusinessEntityID
            ";




            var products = await _context.Set<SaleCustomer>()
                                        .FromSqlRaw(sqlQuery)
                                        .AsNoTracking()    
                                        .ToListAsync();

            return Ok(products);

        }


    
    
     }
}