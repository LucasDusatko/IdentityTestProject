using IdentityTestProject.Infrastructure;
using IdentityTestProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace IdentityTestProject.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<IdentityTestProject.Infrastructure.AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "IdentityTestProject.Infrastructure.AppIdentityDbContext";
        }

        protected override void Seed(IdentityTestProject.Infrastructure.AppIdentityDbContext context)
        {
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));

            string roleName = "Admin";
            string userName = "Admin";
            string password = "MySecret";
            string email = "admin@example.com";

            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }

            AppUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new AppUser { UserName = userName, Email = email }, password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName)
            }

            foreach (AppUser dbUser in userMgr.Users)
            {
                dbUser.City = Cities.PARIS;
            }

            context.SaveChanges();
        }
    }
}
