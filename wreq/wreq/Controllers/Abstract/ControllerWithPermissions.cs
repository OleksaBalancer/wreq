using Microsoft.AspNet.Identity;
using System;
using System.Security.Principal;
using System.Web.Mvc;

namespace wreq.Controllers.Abstract
{
    abstract public class ControllerWithPermissions: Controller
    {
        [Flags]
        protected enum Permissions
        {
            DefaultRecord = 1,
            AuthoredByUser = 2,
            UserIsAdmin = 3
        }

        protected bool CheckPermission(string authorID, Permissions permissions, IPrincipal user)
        {
            bool allowed = false;
            if (permissions.HasFlag(Permissions.DefaultRecord))
            {
                allowed |= authorID == null;
            }
            if (permissions.HasFlag(Permissions.AuthoredByUser))
            {
                allowed |= authorID == user.Identity.GetUserId();
            }
            if (permissions.HasFlag(Permissions.UserIsAdmin))
            {
                allowed |= user.IsInRole("Admin");
            }
            return allowed;
        }
    }
}