using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Order = new HashSet<Order>();
        }

        public string Name { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
