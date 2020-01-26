using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace api.Dto
{
    public class PromotionDTO
    {
        public string Name { get; set; }
        public int MinPrice { get; set; }
        public string PromotionType { get; set; }
    }

    public enum PromotionType
    {
        THIRD_PIZZA_GRATIS,
        THIRTY_PERCENT_OFF,
        FREE_DELIVERY
    }

    public class PizzaDTO
    {
        public string Name { get; set; }

        public virtual List<IngredientsDTO> Ingredients { get; set; }
    }

    public class IngredientsDTO
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }

}
