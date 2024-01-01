using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Data.TMU.User;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Data.TMU.Permissions
{
    public class RolePermission
    {
        [Key]
        public int RP_Id { get; set; }
        public int PermissionId { get; set; }
        public int RoleId { get; set; }

        #region Relation

        public virtual Role Role { get; set; }
        public virtual permission Permission { get; set; }



        #endregion
    }
}
