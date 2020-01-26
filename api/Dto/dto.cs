using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

}
