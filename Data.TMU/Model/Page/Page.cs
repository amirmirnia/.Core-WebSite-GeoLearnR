using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.TMU.Model.Page
{
    public class Page
    {
        [Key]
       public int Id { get; set; }

        [DisplayName("عنوان صفحه")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string TitelPage { get; set; }

        [DisplayName("توضیحات صفحه")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        public string Description { get; set; }

        public String author { get; set; }
        public int? CountView { get; set; }
        public string tag { get; set; }
        [DisplayName("تاریخ خبر")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        public DateTime DetaNews { get; set; }

        public virtual List<FilePage>? FilePages { get; set; }
    }
    public class FilePage
    {
        [Key]
        public int Id { get; set; }

        public string? TitelFilePage { get; set; }

        public string? DescriptionFilePage { get; set; }
        public bool IsFirst { get; set; } = false;

        [DisplayName("مسیر فایل ")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string PathFile { get; set; }

        [ForeignKey("Page")]
        public int IdP { get; set; }
        public virtual Page Page { get; set; }
    }
    public class CategoryPage
    {
        public int IdPage { get; set; }
        public String TitelPage { get; set; }
        public string DescriptionPage { get; set; }
        public DateTime DetaPage { get; set; }
        public string Tags { get; set; }
        public int? CountView { get; set; }
        //public string? Hilight { get; set; } 
        public String author { get; set; }
        public string? FileName { get; set; }
        public string image { get; set; }
    }
    public class ListPageViewModel
    {
        public List<CategoryPage> ListPage { get; set; }
        public int IdPage { get; set; }
        public int CountPage { get; set; }
    }
    public class FilePageViewModel
    {
        public List<IFormFile> files { get; set; }

    }
}
