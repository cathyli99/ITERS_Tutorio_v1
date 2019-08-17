using System;
using System.Collections.Generic;

namespace ITERS_Tutorio_v1.Models
{
    public partial class TbSubscriptionTokens
    {
        public string AuthToken { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
