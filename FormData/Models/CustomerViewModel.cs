using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FormData.Models
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }

        // [RegularExpression]
        // Check this site for info - https://regexone.com/references/csharp 
        //[Required]

        // using my own validation from class below
        // [MyValidation]

        [Required(ErrorMessage = " Text from CustomerViewModel (model state) - Password Required")]
        public string Password { get; set; }

    }

    public class MyValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // or this or your own 
            //return base.IsValid(value);
            return false;
        }
    }
}