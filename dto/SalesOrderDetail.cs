using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDetailApi.Dto
{
    public class SalesOrderDetail
    {
        public int? SalesOrderId { get; set; }
        public int? SalesOrderDetailId { get; set; }
        public string? CarrierTrackingNumber { get; set; }
        public Int16? OrderQty { get; set; }
        public string? ProductName { get; set; }
        // public Int16? SpecialOfferId { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? UnitPriceDiscount { get; set; }
        public decimal? LineTotal { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}