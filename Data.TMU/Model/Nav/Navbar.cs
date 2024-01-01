using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.TMU.Model.Nav
{
    public class Navbar
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("nav عنوان")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(55, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string TitelNav { get; set; }

        [DisplayName("آدرس")]
        [MaxLength(200, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string linkAdres { get; set; }

        public string selectnavbar { get; set; }

        public int ?Rank { get; set; }

        [ForeignKey("Menu")]
        public int ParentId { get; set; } = 0;
    }
    public class ListNavbarViewModel
    {
        public List<Navbar> ListNavbar { get; set; }
        public int IdPage { get; set; }
        public int CountPage { get; set; }
    }
}
