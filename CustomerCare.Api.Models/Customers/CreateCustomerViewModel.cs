using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerCare.Api.Models.Customers
{
    public class CreateCustomerViewModel
    {
        [Required]
        public string CustomerType { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Cellphone { get; set; }
        public double AmountTotal { get; set; }
        [Required]
        public DateTime Date { get; set; }

    }
}
