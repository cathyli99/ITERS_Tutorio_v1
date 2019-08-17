using System;
using System.Collections.Generic;

namespace ITERS_Tutorio_v1.Models
{
    public partial class TbSubscriptionStatuses
    {
        public TbSubscriptionStatuses()
        {
            TbSubscriptions = new HashSet<TbSubscriptions>();
        }

        public int SubscriptionStatusId { get; set; }
        public string SubscriptionStatusName { get; set; }

        public virtual ICollection<TbSubscriptions> TbSubscriptions { get; set; }
    }
}
