using System.Collections.Generic;

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

    public class NewOrderDTO
    {
        public OrderDefinitionDTO PizzaDefinition { get; set; }
        public string Phone { get; set; }

        public bool payOnline { get; set; }
    }

    public class NewOrderResponseDTO
    {
        public string uid { get; set; }

        public int time { get; set; }
    }

    public class OrderDTO
    {
        public string Uid { get; set; }
        public OrderDefinitionDTO PizzaDefinition { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string Phone { get; set; }
        public int Price { get; set; }
    }

    public enum OrderStatus
    {
        WAITING_FOR_PAYMENT,
        IN_REALIZATION,
        WAITING_FOR_DELIVERY,
        IN_DELIVERY,
        DELIVERED
    }

    public class OrderDefinitionDTO
    {
        public List<PizzaOrderDTO> pizza { get; set; }

        public string PromotionName { get; set; }
    }

    public class PizzaOrderDTO
    {
        public string Name { get; set; }

        public virtual List<string> Ingredients { get; set; }
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
