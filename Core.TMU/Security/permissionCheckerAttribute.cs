using System;
using System.Collections.Generic;
using System.Text;
using Core.TMU.Service.ITMUService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Core.TMU.Service.TMUService;
using Data.TMU.Context;



namespace Core.TMU.Security
{
    public class permissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public IPermision _Permision;
        public IUser _user;

        private int _permissionid = 0;
        public permissionCheckerAttribute(int permissionid)
        {
            _permissionid = permissionid;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _Permision = (IPermision)context.HttpContext.RequestServices.GetService(typeof(IPermision));
            _user= (IUser)context.HttpContext.RequestServices.GetService(typeof(IUser));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string username = context.HttpContext.User.Identity.Name;
                var user = _user.FindUser(username);
                if (!_Permision.CheakPermissionAllow(_permissionid, user.Id))
                {
                    context.Result = new RedirectResult("/panel");
                    

                }
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }

    }
}
