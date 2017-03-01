using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arm_repairs_project.Models;
using arm_repairs_project.Models.Data;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace arm_repairs_project.Controllers
{
    [Authorize]  //Контроллер доступен только авторизованным пользователям
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager; //класс для управления пользователями

        public UserController() { }

        public UserController(ApplicationUserManager userManager)
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

        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult Demands()
        {
            List<Demand> demands;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                demands = db.Demands.Include(x=>x.Master).Include(x=>x.Priority).Include(x=>x.Status).ToList();
            }
            var model = new Demands
            {
                DemandsList= demands
            };
            return View(model);
        }
    }
}