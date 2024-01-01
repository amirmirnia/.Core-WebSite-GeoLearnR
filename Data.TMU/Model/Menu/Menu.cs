using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.TMU.Model.Menu
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("عنوان منو")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(55, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string NameMenu { get; set; }
        public int? Priority { get; set; } = 0;

        [ForeignKey("Menu")]
        public int ParentId { get; set; } = 0;

        public virtual List<Menu>? Menus { get; set; }

    }

    public class ListMenuViewModel
    {
        public List<Menu> LidtMenus { get; set; }
        public int IdPage { get; set; }
        public int CountPage { get; set; }
    }

}
