using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using arm_repairs_project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace arm_repairs_project.Controllers
{
    [Authorize]  //Контроллер доступен только авторизованным пользователям
    public class ChiefController : Controller
    {
        private ApplicationUserManager _userManager; //класс для управления пользователями

        public ChiefController() {}

        public ChiefController(ApplicationUserManager userManager )
        {
            UserManager = userManager; //делаем инъекцию через конструктор
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

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
            var model = new ManageUserViewModels.MangeUsers
            {
                Users = users
            };
            return View(model);
        }

        [Authorize(Roles = "chief")]
        public ActionResult UserEdit(string id)
        {
            var user = UserManager.FindById(id);
            var model=new ManageUserViewModels.User();
            if (user != null)
            {
                model.Id = user.Id;
                model.Fio = user.Fio;
                model.UserName = user.UserName;
                model.EmailConfirmed = user.EmailConfirmed;
                model.Email = user.Email;
                model.IsChief = UserManager.GetRoles(user.Id).Contains("chief");
                model.IsManager = UserManager.GetRoles(user.Id).Contains("manager");
                model.IsMaster= UserManager.GetRoles(user.Id).Contains("master");
                model.IsUser= UserManager.GetRoles(user.Id).Contains("user");
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(ManageUserViewModels.User model)
        {

            return null;
        }

    }
}
