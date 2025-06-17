using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleCustomerApi.Dto
{
    public class SaleCustomer
    {
        public int? CustomerID { get; set; }
        public int? PersonID { get; set; }
        public string? Name { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}