using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Data.TMU.Permissions
{
    public class permission
    {
        [Key]
        public int PermissionId { get; set; }
        [DisplayName("نام نقش")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0}نمی تواند بیشتر از {1}باشد")]
        public string PermissionTitel { get; set; }

        public int? ParentId { get; set; }



        #region Relation
        [ForeignKey("ParentId")]
        public List<permission> Permissions { get; set; }

        public virtual List<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
