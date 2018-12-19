using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormData.Models
{
    public class CartDTO
    {
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public int? ProductId { get; set; }
        public int? CustomerId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string ProductName { get; set; }
        public decimal? Total { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        

    } // end of CartDTO
}