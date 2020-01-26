using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class PizzaIntegrients
    {
        public int Id { get; set; }
        public string PizzaName { get; set; }
        public string IngredientName { get; set; }

        public virtual Ingredient IngredientNameNavigation { get; set; }
        public virtual PizzaDefinition PizzaNameNavigation { get; set; }
    }
}
