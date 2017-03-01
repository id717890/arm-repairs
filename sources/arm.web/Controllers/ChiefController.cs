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
        [Authorize(Roles = "chief")]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(ManageUserViewModels.User model)
        {
            //Проверяем модель на валидность
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = UserManager.FindById(model.Id);
            //Если пользователь не найден то возвращаем ошибку
            if (user == null)
            {
                ModelState.AddModelError("", "Пользователь не найден.");
                return View(model);
            }

            //Ищем пользователя с таким же email
            var email = UserManager.FindByEmail(model.Email);
            //Если найден пользователь с таким же email и это другой пользователь то возварщаем ошибку
            if ((email != null) && (email.Id != user.Id))
            {
                ModelState.AddModelError("", "Пользователь с таким Email уже существует.");
                return View(model);
            }

            //Ищем пользователя с таким же UserName
            var userName = UserManager.FindByName(model.UserName);
            //Если найден пользователь с таким же user name и это другой пользователь то возварщаем ошибку
            if ((userName != null) && (userName.Id != user.Id))
            {
                ModelState.AddModelError("", "Пользователь с таким UserName уже существует.");
                return View(model);
            }

            user.Fio = model.Fio;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.EmailConfirmed = model.EmailConfirmed;
            UserManager.Update(user);

            //Присваиваем или удалем роли у пользователя
            if (model.IsChief) UserManager.AddToRole(model.Id, "chief"); else UserManager.RemoveFromRole(model.Id, "chief");
            if (model.IsManager) UserManager.AddToRole(model.Id, "manager"); else UserManager.RemoveFromRole(model.Id, "manager");
            if (model.IsMaster) UserManager.AddToRole(model.Id, "master"); else UserManager.RemoveFromRole(model.Id, "master");
            if (model.IsUser) UserManager.AddToRole(model.Id, "user"); else UserManager.RemoveFromRole(model.Id, "user");

            TempData["success"] = "Данные успешно сохранены";
            return View(model);
        }

        [Authorize(Roles = "chief")]
        public ActionResult UserDelete(string id)
        {
            var user = UserManager.FindById(id);
            var model = new ManageUserViewModels.User();
            if (user != null)
            {
                model.Id = user.Id;
                model.Fio = user.Fio;
                model.UserName = user.UserName;
                model.EmailConfirmed = user.EmailConfirmed;
                model.Email = user.Email;
                model.IsChief = UserManager.GetRoles(user.Id).Contains("chief");
                model.IsManager = UserManager.GetRoles(user.Id).Contains("manager");
                model.IsMaster = UserManager.GetRoles(user.Id).Contains("master");
                model.IsUser = UserManager.GetRoles(user.Id).Contains("user");
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "chief")]
        [ValidateAntiForgeryToken]
        public ActionResult UserDelete(ManageUserViewModels.User model)
        {
            //Проверяем модель на валидность
            if ((model.Id==null) || (model.Id == String.Empty))
            {
                ModelState.AddModelError("", "Идентификатор пользователя не определен.");
                return View(model);
            }
            var user = UserManager.FindById(model.Id);
            //Если пользователь не найден то возвращаем ошибку
            if (user == null)
            {
                ModelState.AddModelError("", "Пользователь не найден.");
                return View(model);
            }
            UserManager.Delete(user);
            return RedirectToAction("Users","Chief");
        }

    }
}
