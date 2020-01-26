using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class PromotionType
    {
        public PromotionType()
        {
            Promotion = new HashSet<Promotion>();
        }

        public string Name { get; set; }

        public virtual ICollection<Promotion> Promotion { get; set; }
    }
}
