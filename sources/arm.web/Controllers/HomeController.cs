﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace arm_repairs_project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "chief")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "master")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}