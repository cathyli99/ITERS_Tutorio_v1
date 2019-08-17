using System;
using System.Collections.Generic;

namespace ITERS_Tutorio_v1.Models
{
    public partial class TbCourseAssignments
    {
        public Guid InstructorId { get; set; }
        public int CourseId { get; set; }
    }
}
