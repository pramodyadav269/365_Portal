using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _365_Portal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Response.Redirect("~/login.aspx");
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
