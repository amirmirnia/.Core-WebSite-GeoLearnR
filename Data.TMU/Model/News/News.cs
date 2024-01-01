using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.TMU.Model.News
{
    public class News
    {
        [Key]
        public int IdNews { get; set; }

        [DisplayName("عنوان خبر")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public String TitelNews { get; set; }

        [DisplayName("هایلایت")]
        [MaxLength(15, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public String? Mark { get; set; }

        [DisplayName("تیتر خبر")]
        [MaxLength(180, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public String? SubjectNews { get; set; }

        [DisplayName("متن خبر")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string DescriptionNews { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Description1 { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string? Description2 { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Description3 { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Description4 { get; set; }

        [DisplayName("تاریخ خبر")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        public DateTime DetaNews { get; set; }

        [DisplayName("تگ ")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string Tags { get; set; }

        public int? CountView { get; set; }
        //public string? Hilight { get; set; } 

        public String author { get; set; }

        [DisplayName("فایل ضمیمه")]
        [MaxLength(100, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string? FileName { get; set; }

        public bool IsSearch { get; set; }

        public virtual List<FileNews>? FileNews { get; set; }
    }
    public class CategoryNews
    {
        public int IdNews { get; set; }
        public String TitelNews { get; set; }
        public string DescriptionNews { get; set; }
        public String? Mark { get; set; }
        public string? subjectNews { get; set; }
        public DateTime DetaNews { get; set; }
        public string Tags { get; set; }
        public int? CountView { get; set; }
        //public string? Hilight { get; set; } 
        public String author { get; set; }
        public string? FileName { get; set; }
        public string image { get; set; }
    }
    public class ListNewsViewModel
    {
        public List<CategoryNews> ListNews { get; set; }
        public int IdPage { get; set; }
        public int CountPage { get; set; }
    }
    public class FileNewsViewModel
    {
        public List<IFormFile> files { get; set; }

    }
    public class FileNews
    {
        [Key]
        public int Id { get; set; }

        public string? TitelFileNews { get; set; }

        public string? DescriptionFileNews { get; set; }
        public bool IsFirst { get; set; } = false;

        [DisplayName("مسیر فایل ")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string PathFile { get; set; }

        [ForeignKey("News")]
        public int IdN { get; set; }
        public virtual News News { get; set; }
    }
    public class NewsViewModel
    {
        public News News { get; set; }
    }
    }
