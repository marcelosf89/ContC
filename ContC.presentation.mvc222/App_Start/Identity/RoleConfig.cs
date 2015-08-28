using ContC.presentation.mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.presentation.mvc.App_Start.Identity
{
    public class RoleConfig : RoleManager<ApplicationRoles>
    {
        public RoleConfig(IRoleStore<ApplicationRoles> store)
            : base(store)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationRoleManager(new RoleStore<ApplicationRoles>(context.Get<ApplicationDbContext>()));

            //manager. = new UserValidator<ApplicationUser>(manager)
            //{
            //    AllowOnlyAlphanumericUserNames = false,
            //    RequireUniqueEmail = true
            //};


            return manager;
        }
    }
}