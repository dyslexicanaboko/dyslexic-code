using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskListPoolWebApi.Controllers
{
    public class MillionThingsVjsController : Controller
    {
        // GET: MillionThingsVjs
        public ActionResult Index()
        {
            ViewBag.RefreshTime = DateTime.Now.ToString();

            return View();
        }
    }
}