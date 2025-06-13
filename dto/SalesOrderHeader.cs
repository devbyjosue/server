using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SalesApi.Dto
{
    public class SalesOrderHeader
    {
    public int SalesOrderId { get; set; }
    public byte RevisionNumber { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public byte? Status { get; set; }
    public string? SalesOrderNumber { get; set; }
    public string? PurchaseOrderNumber { get; set; }
    public string? AccountNumber { get; set; } 
    // public int? CustomerId { get; set; }
    // public int? SalesPersonId { get; set; }
    // public int? TerritoryId { get; set; }
    // public int? BillToAddressId { get; set; }
    // public int? ShipToAddressId { get; set; }
    // public int? ShipMethodId { get; set; }
    // public int? CreditCardId { get; set; }
    // public string? CreditCardApprovalCode { get; set; } 
    // public byte? CurrencyRateId { get; set; }
    public decimal? SubTotal { get; set; }
    public decimal? TaxAmt { get; set; }
    public decimal? Freight { get; set; }
    public decimal? TotalDue { get; set; }
    public string? Comment { get; set; }
    // public string? rowguid { get; set; }
    public DateTime? ModifiedDate { get; set; }

    }
}