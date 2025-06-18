using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using SalesDetailApi.Dto;
using server.Models;

namespace SalesOrderRequestApi.Dto
{
    public class SalesOrderRequest
    {
        public SalesOrderHeader SalesOrderHeader { get; set; }
        public List<SalesOrderDetail> SalesOrderDetail { get; set; }
    }
}