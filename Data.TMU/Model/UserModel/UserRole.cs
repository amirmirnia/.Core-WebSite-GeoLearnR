using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.TMU.User
{
    public class UserRole
    {
       

        [Key]
        public int UR_Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }


        #region Relations

        public virtual Model.User User { get; set; }
        public virtual Role Role { get; set; }

        #endregion

    }
}
