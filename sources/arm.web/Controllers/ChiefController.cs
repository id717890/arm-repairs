using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using arm_repairs_project.Models;

namespace arm_repairs_project.Controllers
{
    [Authorize]  //Контроллер доступен только авторизованным пользователям
    public class ChiefController : Controller
    {
        [Authorize(Roles = "chief")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "chief")]
        public ActionResult Users()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                users = db.Users.ToList();
            }
            var model = new MangeUserViewModels.MangeUsers
            {
                Users = users
            };
            return View(model);
        }

    }
}
