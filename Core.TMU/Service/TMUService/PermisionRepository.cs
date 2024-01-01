using Core.TMU.Service.ITMUService;
using Data.TMU.Context;
using Data.TMU.Permissions;
using Data.TMU.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.TMU.Service.TMUService
{
    public class PermisionRepository : IPermision
    {
        public ContextTMU _Context { get; set; }

        public PermisionRepository(ContextTMU Context)
        {
            _Context = Context;
        }

        public List<Role> GetAllRole()
        {
            return _Context.Roles.Where(p=>p.IsDelete==false).ToList();
        }

        public void AddRole(List<int> roles, int userid)
        {

            foreach (var Item in roles)
            {
                
                var o = _Context.UserRoles.FirstOrDefault(p => p.UserId == userid && p.RoleId == Item);
                if (o==null)
                {
                    _Context.UserRoles.Add(new UserRole()
                    {
                        UserId = userid,
                        RoleId = Item
                    });
                    
                }
                
                
            }
            
            _Context.SaveChanges();
        }

        public void EditeRole(List<int> roles, int userid)
        {
            _Context.UserRoles.Where(r => r.UserId == userid).ToList().ForEach(r => _Context.UserRoles.Remove(r));
            AddRole(roles, userid);

        }

        public Role GetRoleById(int id)
        {
            return _Context.Roles.Find(id);
        }

        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            UpdateRole(role);
        }

        public void UpdateRole(Role role)
        {
            _Context.Update(role);
            _Context.SaveChanges();
        }

        public int CreatRole(Role role)
        {
            if (GetFinalRole() == null)
            {
                role.Finalapproval = true;
            }
            _Context.Roles.Add(role);
            _Context.SaveChanges();
            return role.RoleId;
        }

        public List<permission> GetAllPermission()
        {
            return _Context.Permissions.ToList();
        }

        public void AddPermission(List<int> permission, int roleid)
        {
            foreach (var item in permission)
            {
                _Context.RolePermissions.Add(new RolePermission()
                {
                    PermissionId = item,
                    RoleId = roleid
                });
            }
            _Context.SaveChanges();

        }

        public List<int> GetpermissionWhitRoleId(int roleid)
        {
            return _Context.RolePermissions
                .Where(p => p.RoleId == roleid)
                .Select(p => p.PermissionId).ToList();
        }

        public void UpdatePermissionRole(List<int> permission, int roleid)
        {
            _Context.RolePermissions
                .Where(p => p.RoleId == roleid)
                .ToList().ForEach(p => _Context.RolePermissions.Remove(p));

            AddPermission(permission, roleid);
        }

        public bool CheakPermissionAllow(int permissionid, int userid)
        {
            var user = _Context.Users.SingleOrDefault(u => u.Id == userid);

            if (user.IsAdmin== true)
            {
                return true;
            }
            else
            {
                //list role one user
                List<int> userrole = _Context.UserRoles
                    .Where(u => u.UserId == userid)
                    .Select(p => p.RoleId).ToList();
                if (userrole == null)
                    return false;

                //list roles ke daraye in permissionid hastan
                List<int> roleList = _Context.RolePermissions
                    .Where(p => p.PermissionId == permissionid)
                    .Select(r => r.RoleId).ToList();


                return roleList.Any(p => userrole.Contains(p));
            }
        }

        public Role GetRoleByName(string rolename)
        {
            Role role = _Context.Roles.SingleOrDefault(p => p.RoleTitle == rolename);
            if (role==null)
            {
                return null;
            }
            else
            {
                return _Context.Roles.Find(role.RoleId);
            }

           
        }

        public List<int> GetUserWhitRoleId(int PkIdRoleId)
        {
            return _Context.UserRoles
               .Where(p => p.RoleId == PkIdRoleId)
               .Select(p => p.UserId).ToList();
        }

        public List<SelectListItem> GetRole()
        {
            return _Context.Roles.Where(p => p.IsDelete == false).Select(p=>new SelectListItem()
            {
                Text=p.RoleTitle,
                Value= p.RoleId.ToString(),
            }).ToList();
            
        }

        public string GetlevelRole(int roleid)
        {
            
            var level= _Context.Roles.FirstOrDefault(p => p.RoleId == roleid && p.IsDelete == false).Level;
            if (level!=null)
            {
                return level;
            }
            return null;
        }

        public Role GetFinalRole()
        {
            var final = _Context.Roles.FirstOrDefault(p => p.Finalapproval == true && p.IsDelete == false);
            if (final!=null)
            {
                return final;
            }

            return null;
        }

        public bool IsTrueFinalaproval()
        {
            var final = _Context.Roles.FirstOrDefault(p => p.Finalapproval == true && p.IsDelete == false);
            if (final != null)
            {
                return true;
            }

            return false;

        }

        public Role GetPermitionPrintRole()
        {
            var PPrint = _Context.Roles.FirstOrDefault(p => p.PermissionPrint == true && p.IsDelete == false);
            if (PPrint != null)
            {
                return PPrint;
            }

            return null;
        }

        public bool IsTruePermitionPrint()
        {
            var PPrint = _Context.Roles.FirstOrDefault(p => p.PermissionPrint == true && p.IsDelete == false);
            if (PPrint != null)
            {
                return true;
            }

            return false;
        }

        public bool GetRoleWhitLevel(string level)
        {
            var levell = _Context.Roles.FirstOrDefault(p => p.Level == level && p.IsDelete==false);
            if (levell != null)
            {
                return true;
            }

            return false;
        }

        public async Task UpdateRoleTask(Role role)
        {
            _Context.Update(role);
            _Context.SaveChanges();
        }

        public Tuple<string, string> GetminmaxLevel()
        {
            var min = "0";
            var max = "0";
             min = _Context.Roles.Where(p=>p.IsDelete == false).Min(p => p.Level);
             max= _Context.Roles.Where(p => p.IsDelete == false).Max(p => p.Level);
            return  Tuple.Create(min, max);
        }

        public bool IsTruePermitioncheckout()
        {
            var PPrint = _Context.Roles.FirstOrDefault(p => p.Checkout == true && p.IsDelete == false);
            if (PPrint != null)
            {
                return true;
            }

            return false;
        }

        public Role Getcheckouttrue()
        {
            var Role = _Context.Roles.FirstOrDefault(p => p.Checkout == true && p.IsDelete == false);
            if (Role != null)
            {
                return Role;
            }

            return null;
        }

        public int GetMaxLevel()
        {
            try
            {
                var d = _Context.Roles.Max(n => n.Level);
                return int.Parse(d);
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public bool Isendprosecc()
        {
            var EndProcess = _Context.Roles.FirstOrDefault(p => p.EndProcess == true && p.IsDelete == false);
            if (EndProcess != null)
            {
                return true;
            }

            return false;
        }

        public Role GetRoleWhitLevellist(string level)
        {
            try
            {
                return _Context.Roles.First(p => p.Level == level && p.IsDelete == false);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Role Getsignuchure()
        {
            var Signuchure = _Context.Roles.FirstOrDefault(p => p.Signuchure == true && p.IsDelete == false);
            if (Signuchure != null)
            {
                return Signuchure;
            }

            return null;
        }

        public bool Issignuchure()
        {
            var Signuchure = _Context.Roles.FirstOrDefault(p => p.Signuchure == true && p.IsDelete == false);
            if (Signuchure != null)
            {
                return true;
            }

            return false;
        }

        public Role GetPermitionEndprosecc()
        {
            var EndProcess = _Context.Roles.FirstOrDefault(p => p.EndProcess == true && p.IsDelete == false);
            if (EndProcess != null)
            {
                return EndProcess;
            }

            return null;
        }
    }
}

