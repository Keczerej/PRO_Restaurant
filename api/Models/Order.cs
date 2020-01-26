using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Order
    {
        public string Uid { get; set; }
        public string PizzaDefinition { get; set; }
        public string OrderStatusName { get; set; }
        public string Phone { get; set; }
        public int Price { get; set; }

        public virtual OrderStatus OrderStatusNameNavigation { get; set; }
    }
}
