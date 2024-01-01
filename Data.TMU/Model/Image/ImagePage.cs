using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.TMU.Model.Image
{
    public class ImagePage
    {
        public int Id { get; set; }
        [DisplayName("عنوان عکس ")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public String Titel { get; set; }
        public string? Description { get; set; }
        public DateTime DetaPage { get; set; }
        public string Tags { get; set; }
        public int CountView { get; set; } = 0;
        //public string? Hilight { get; set; } 
        public String author { get; set; }

        [DisplayName("مسیر فایل ")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string PathFile { get; set; }

    }
    public class ListImagePageViewModel
    {
        public List<ImagePage> ListimagePages { get; set; }
        public int IdPage { get; set; }
        public int CountPage { get; set; }
    }
    public class ImagePageViewModel
    {
        public ImagePage ImagePage { get; set; }
        public List<IFormFile> files { get; set; }

    }
}
