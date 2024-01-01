using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.TMU.Model.Course
{
    public class CourseComment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Course")]
        public int idC { get; set; }
        [ForeignKey("User")]
        public int idU { get; set; }
        [DisplayName("کامنت")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(600, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string comment { get; set; }
        public DateTime Date { get; set; }
        public bool IsAllow { get; set; }


        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
