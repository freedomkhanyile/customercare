using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCare.Data.Models
{
    public class CustomerType
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }
    }
}
