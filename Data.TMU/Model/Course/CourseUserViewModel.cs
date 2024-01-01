using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.TMU.Model.Course
{
    public class CourseUserViewModel
    {
        public int IdCourse { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public string img { get; set; }
    }
    public class ListCourseUserViewModel
    {
        public List<CourseUserViewModel> CourseUserViewModel { get; set; }
        public int IdPage { get; set; }
        public int CountPage { get; set; }
    }
}
