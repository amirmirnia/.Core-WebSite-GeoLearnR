using Data.TMU.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.TMU.User
{
   public class Role
    {

        [Key]
        public int RoleId { get; set; }

        [Display(Name = "نام نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string RoleTitle { get; set; }

        [Display(Name = "سطح")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(2, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Level { get; set; }

        public bool Finalapproval { get; set; }
        public bool PermissionPrint { get; set; }
        public bool EndProcess{ get; set; }
        public bool Signuchure { get; set; }

        public bool Checkout { get; set; }

        public bool IsDelete { get; set; }



        #region Relations

        public virtual List<UserRole>? UserRoles { get; set; }
        //public virtual List<Permissions.RolePermission> RolePermissions { get; set; }



        #endregion
    }
}
