using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskListPoolWebApi.Controllers
{
    public class MillionThingsController : Controller
    {
        // GET: MillionThings
        public ActionResult Index()
        {
            return View();
        }
    }
}