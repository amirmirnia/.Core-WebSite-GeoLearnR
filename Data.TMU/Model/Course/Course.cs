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
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("نام دوره")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string name { get; set; }
        [DisplayName("عنوان دوره")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(60, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string Titel { get; set; }

        [DisplayName("توضیح دوره")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        public string? Description { get; set; }

        public string author { get; set; }
        [DisplayName("قیمت دوره")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string price { get; set; }

        [DisplayName("ساعت ")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string Time { get; set; }
        [DisplayName("دقیقه")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string minet { get; set; }
        [DisplayName("ثانیه")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string secend { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("وضعیت دوره")]
        public bool Status { get; set; }
        [DisplayName("سطح دوره")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string level { get; set; }
        [DisplayName("عکس دوره")]
        public string img { get; set; }
        [DisplayName("تگ دوره")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string Tag { get; set; }
        public bool Lock { get; set; } = false;

        public virtual List<CourseComment>? CourseComment { get; set; }
        public virtual List<FileCourse>? FileCourse { get; set; }

        public virtual List<UserCourse>? UserCourse { get; set; }
    }

    public class FileCourse
    {
        [Key]
        public int IdFC { get; set; }
        [ForeignKey("Course")]
        public int IdC { get; set; }
        public string author { get; set; }
        public bool Lock { get; set; }
        [DisplayName("نام ویدیو")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string name { get; set; }
        
        [DisplayName("ساعت ")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string hour { get; set; }
        [DisplayName("دقیقه")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string minet { get; set; }
        [DisplayName("ثانیه")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string secend { get; set; }
        
        public DateTime Date { get; set; }
        public string? file { get; set; }

        public virtual Course? Course { get; set; }
    }
    public class Categorycorse
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public string Titel { get; set; }
        public string author { get; set; }
        public string price { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public string img { get; set; }
        [DisplayName("ساعت ")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string Time { get; set; }
        public string minet { get; set; }

        public string secend { get; set; }
        public bool Lock { get; set; }
    }
    public class ListCourseViewModel
    {
        public List<Categorycorse> Listcourse { get; set; }
        public int IdPage { get; set; }
        public int CountPage { get; set; }
    }
    public class ListCourseVideoViewModel
    {
        public List<FileCourse> ListcourseVideo { get; set; }
        public int IdPage { get; set; }
        public int CountPage { get; set; }
    }
}
