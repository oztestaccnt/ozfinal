using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FormData.Models
{
    public class Customer
    {
        // [Required] - it's an attribute 
        [Required] public string FName { get; set; }
        [Required] public string LName { get; set; }
        [Required] public string PhoneNumber { get; set; }
        [Required] public string Address { get; set; }
        [Required] public string EmailAddress { get; set; }

        public Customer(string fN, string lN)
        {
            FName = fN;
            LName = lN;
        }

        public Customer(string fN, string lN, string addr)
        {
            FName = fN;
            LName = lN;
            Address = addr;
        }

        public Customer(string fN, string lN, string addr, string emAddress, string phNumb)
        {
            FName = fN;
            LName = lN;
            PhoneNumber = phNumb;
            Address = addr;
            EmailAddress = emAddress;
        }

    } // end of Customer class
}