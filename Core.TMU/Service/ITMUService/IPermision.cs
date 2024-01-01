using System;
using System.Collections.Generic;
using System.Text;
using Data.TMU.Permissions;
using Data.TMU.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Migrations.Operations;


namespace Core.TMU.Service.ITMUService
{
   public interface IPermision
   {


        #region Role

        List<Role> GetAllRole();
        void AddRole(List<int> roles, int userid);
        void EditeRole(List<int> roles, int userid);
        Role GetRoleById(int id);
        Tuple<string, string> GetminmaxLevel();
        Role GetFinalRole();
        Role Getsignuchure();
        Role Getcheckouttrue();
        bool GetRoleWhitLevel(string level);
        Role GetRoleWhitLevellist(string level);
        Role GetPermitionPrintRole();
        Role GetPermitionEndprosecc();
        void DeleteRole(Role role);
        void UpdateRole(Role role);
        Task UpdateRoleTask(Role role);
        int CreatRole(Role role);
        bool IsTrueFinalaproval();
        bool Isendprosecc();
        bool Issignuchure();
        bool IsTruePermitionPrint();
        bool IsTruePermitioncheckout();
        Role GetRoleByName(string rolename);
        List<int> GetUserWhitRoleId(int PkIdRoleId);
        string GetlevelRole(int roleid);
        List<SelectListItem> GetRole();
        int GetMaxLevel();

        #endregion

        #region permission

        List<permission> GetAllPermission();
        void AddPermission(List<int> permission, int roleid);
        List<int> GetpermissionWhitRoleId(int roleid);
        void UpdatePermissionRole(List<int> permission, int roleid);
        bool CheakPermissionAllow(int permissionid, int userid);


        #endregion


    }
}
