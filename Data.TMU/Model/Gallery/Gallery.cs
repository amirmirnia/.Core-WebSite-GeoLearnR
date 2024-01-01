using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.TMU.Model.Gallery
{
    public class Gallery
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("عنوان گالری")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string Titel { get; set; }
        public string ?Description { get; set; }

        public int View { get; set; } = 0;
        public string author { get; set; }
        public DateTime DateGallery { get; set; }
        public string ?Tag { get; set; }
        public virtual List<FileGallery>? FileGalleries { get; set; }

    }
    public class FileGalleryViewModel
    {
        public List<IFormFile> files { get; set; }

    }
    public class FileGallery
    {
        [Key]
        public int Id { get; set; }

        public string? TitelFileGallery { get; set; }

        public string? DescriptionFileGallery { get; set; }
        public bool IsFirst { get; set; } = false;

        [DisplayName("مسیر فایل ")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string PathFile { get; set; }

        [ForeignKey("Gallery")]
        public int IdG { get; set; }
        public virtual Gallery Gallery { get; set; }
    }
    public class CategoryGallery
    {
        public int IdGallery { get; set; }
        public String ?Titel { get; set; }
        public string ?Description { get; set; }
        public DateTime DateGallery { get; set; }
        public string ?Tag { get; set; }
        public int? View { get; set; }
        //public string? Hilight { get; set; } 
        public String author { get; set; }
        public string? FileName { get; set; }
        public string image { get; set; }
    }

    public class ListGalleryViewModel
    {
        public List<CategoryGallery> ListGallery { get; set; }
        public int IdPage { get; set; }
        public int CountPage { get; set; }
    }
}
