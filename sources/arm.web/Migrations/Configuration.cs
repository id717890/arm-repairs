using arm_repairs_project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace arm_repairs_project.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<arm_repairs_project.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(arm_repairs_project.Models.ApplicationDbContext context)
        {
            var storeRole = new RoleStore<IdentityRole>(context);
            var managerRole = new RoleManager<IdentityRole>(storeRole);
            if (!context.Roles.Any(r => r.Name == "chief"))
            {
                var role = new IdentityRole { Name = "chief" };
                managerRole.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "manager"))
            {
                var role = new IdentityRole { Name = "manager" };
                managerRole.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "master"))
            {
                var role = new IdentityRole { Name = "master" };
                managerRole.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "user"))
            {
                var role = new IdentityRole { Name = "user" };
                managerRole.Create(role);
            }

            var storeUser = new UserStore<ApplicationUser>(context);
            var managerUser = new UserManager<ApplicationUser>(storeUser);
            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var user = new ApplicationUser
                {
                    Id = "1000", EmailConfirmed = true,
                    Email = "jusupovz@gmail.com",
                    UserName = "admin",
                    Fio = "‘»Œ 1"
                };
                managerUser.Create(user, "123456");
                managerUser.AddToRole(user.Id, "chief");
            }
            if (!context.Users.Any(u => u.UserName == "manager"))
            {
                var user = new ApplicationUser
                {
                    Id = "1001",
                    EmailConfirmed = true,
                    Email = "manager@gmail.com",
                    UserName = "manager",
                    Fio = "‘»Œ 2"
                };
                managerUser.Create(user, "123456");
                managerUser.AddToRole(user.Id, "manager");
            }
            if (!context.Users.Any(u => u.UserName == "master"))
            {
                var user = new ApplicationUser
                {
                    Id = "1002",
                    EmailConfirmed = true,
                    Email = "master@gmail.com",
                    UserName = "master",
                    Fio = "‘»Œ 3"
                };
                managerUser.Create(user, "123456");
                managerUser.AddToRole(user.Id, "master");
            }
            if (!context.Users.Any(u => u.UserName == "user"))
            {
                var user = new ApplicationUser
                {
                    Id = "1003",
                    EmailConfirmed = true,
                    Email = "user@gmail.com",
                    UserName = "user",
                    Fio = "‘»Œ 4"
                };
                managerUser.Create(user, "123456");
                managerUser.AddToRole(user.Id, "user");
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
