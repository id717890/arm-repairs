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
                    Id = "1000",
                    EmailConfirmed = true,
                    Email = "jusupovz@gmail.com",
                    UserName = "admin",
                    Fio = "ФИО 1"
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
                    Fio = "ФИО 2"
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
                    Fio = "ФИО 3"
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
                    Fio = "ФИО 4"
                };
                managerUser.Create(user, "123456");
                managerUser.AddToRole(user.Id, "user");
            }

            #region Инициализируем статусы заявок
            context.DemandStatuses.AddOrUpdate(
                    x => x.Id,
                    new Models.Data.DemandStatus { Id = 1, Caption = "Ожидание подтверждения менеджером" },
                    new Models.Data.DemandStatus { Id = 2, Caption = "Ожидание мастера" },
                    new Models.Data.DemandStatus { Id = 3, Caption = "Заказ запчасти" },
                    new Models.Data.DemandStatus { Id = 4, Caption = "В работе" },
                    new Models.Data.DemandStatus { Id = 5, Caption = "Выполнено" },
                    new Models.Data.DemandStatus { Id = 6, Caption = "Отменен" }
                    ); 
            #endregion

            #region Инициализируем приоритеты
            context.Priorities.AddOrUpdate(
                    x => x.Id,
                    new Models.Data.Priority { Id = 1, Caption = "Высокий" },
                    new Models.Data.Priority { Id = 2, Caption = "Средний" },
                    new Models.Data.Priority { Id = 3, Caption = "Низкий" }
                    ); 
            #endregion
            context.SaveChanges();

            #region Инициализируем заявки
            context.Demands.AddOrUpdate(
                    x => x.Id,
                    new Models.Data.Demand
                    {
                        Id = 1,
                        Date = DateTime.Now,
                        DescriptionIssue = "Описание проблемы 1",
                        Phone = "0500",
                        User = context.Users.SingleOrDefault(x => x.Id == "1003"),
                        Manager = null,
                        Master = null,
                        DecisionHours = 3,
                        DecisionDescription = "Описание",
                        Equipment = "Оборудование",
                        Priority = context.Priorities.SingleOrDefault(x => x.Id == 1),
                        Status = context.DemandStatuses.SingleOrDefault(x => x.Id == 1)
                    }
                    );
            context.Demands.AddOrUpdate(
                x => x.Id,
                new Models.Data.Demand
                {
                    Id = 1,
                    Date = DateTime.Now,
                    DescriptionIssue = "Описание проблемы 2",
                    Phone = "0500",
                    User = context.Users.SingleOrDefault(x => x.Id == "1003"),
                    Manager = null,
                    Master = null,
                    DecisionHours = 3,
                    DecisionDescription = "Описание 2",
                    Equipment = "Оборудование 2",
                    Priority = context.Priorities.SingleOrDefault(x => x.Id == 1),
                    Status = context.DemandStatuses.SingleOrDefault(x => x.Id == 2)
                }
                );
            context.Demands.AddOrUpdate(
                x => x.Id,
                new Models.Data.Demand
                {
                    Id = 1,
                    Date = DateTime.Now,
                    DescriptionIssue = "Описание проблемы 3",
                    Phone = "0500",
                    User = context.Users.SingleOrDefault(x => x.Id == "1003"),
                    Manager = null,
                    Master = null,
                    DecisionHours = 3,
                    DecisionDescription = "Описание 3",
                    Equipment = "Оборудование 3",
                    Priority = context.Priorities.SingleOrDefault(x => x.Id == 1),
                    Status = context.DemandStatuses.SingleOrDefault(x => x.Id == 3)
                }
                ); 
            #endregion



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
