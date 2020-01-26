using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class PizzaDefinition
    {
        public PizzaDefinition()
        {
            PizzaIntegrients = new HashSet<PizzaIntegrients>();
        }

        public string Name { get; set; }

        public virtual ICollection<PizzaIntegrients> PizzaIntegrients { get; set; }
    }
}
