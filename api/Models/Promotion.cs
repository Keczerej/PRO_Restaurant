using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Promotion
    {
        public string Name { get; set; }
        public int MinPrice { get; set; }
        public string PromotionTypeName { get; set; }

        public virtual PromotionType PromotionTypeNameNavigation { get; set; }
    }
}
