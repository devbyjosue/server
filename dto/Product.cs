using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Dto
{
    public class Product
    {
        public int? ProductID { get; set; }
        public string? Name { get; set; }
        public string? ProductNumber { get; set; }
        public decimal? StandardCost { get; set; }
        public decimal? ListPrice { get; set; }
        public DateTime? SellStartDate { get; set; }
        public DateTime? SellEndDate { get; set; }


    }
}