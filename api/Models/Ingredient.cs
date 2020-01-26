using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            PizzaIntegrients = new HashSet<PizzaIntegrients>();
        }

        public string Name { get; set; }
        public int Price { get; set; }

        public virtual ICollection<PizzaIntegrients> PizzaIntegrients { get; set; }
    }
}
