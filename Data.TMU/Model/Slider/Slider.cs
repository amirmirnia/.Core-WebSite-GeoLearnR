using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.TMU.Model.Slider
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("عنوان اسلایدر")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string NameSlider { get; set; }
        public string? Link { get; set; }
        [DisplayName("فایل ضمیمه")]
        [MaxLength(100, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string? FileName { get; set; }
        public DateTime DetaSlider { get; set; }
        public bool IsActive { get; set; }
        public bool ShowTitel { get; set; }

    }
    public class SliderViewModel
    {
        public Slider  slider{ get; set; }
        public IFormFile? fileimage { get; set; }
    }
    public class ListSliderViewModel
    {
        public List<Slider> ListSlider { get; set; }
        public int IdPage { get; set; }
        public int CountPage { get; set; }
    }
}
