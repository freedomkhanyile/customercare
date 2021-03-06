using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCare.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public double AmountTotal { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }

    }
}
