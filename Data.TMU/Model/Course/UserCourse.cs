using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.TMU.Model.Course
{
    public class UserCourse
    {
        [Key]
        public int UCId { get; set; }

        [ForeignKey("User")]
        public int userid { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public string CodeId { get; set; }

        public virtual User User { get; set; }
        public virtual Course Course { get; set; }
    }
}
