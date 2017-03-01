using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace arm_repairs_project.Models
{
    public static class Assists
    {
        public static List<ApplicationUser> GetUsersInRole(string roleName)
        {
            List<ApplicationUser> usersInRole;
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var role = roleManager.FindByName(roleName).Users.First();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                usersInRole = db.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId)).ToList();
            }
            return usersInRole;
        }
    }
}