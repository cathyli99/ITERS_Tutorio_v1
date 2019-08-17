using System;
using System.Collections.Generic;

namespace ITERS_Tutorio_v1.Models
{
    public partial class TbCourseSubscriptions
    {
        public Guid StudentId { get; set; }
        public int CourseId { get; set; }
        public byte SubscriptionStatus { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
