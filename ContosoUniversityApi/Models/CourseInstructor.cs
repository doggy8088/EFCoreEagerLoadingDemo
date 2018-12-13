using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ContosoUniversityApi.Models
{
    public partial class CourseInstructor
    {
        public int CourseId { get; set; }
        public int InstructorId { get; set; }

        [JsonIgnore]
        public virtual Course Course { get; set; }
        public virtual Person Instructor { get; set; }
    }
}
