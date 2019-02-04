using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MySuper.Api.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public DateTime OrderDate { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
       
      
    }
}
