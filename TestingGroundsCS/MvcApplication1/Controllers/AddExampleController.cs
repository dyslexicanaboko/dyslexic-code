using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class AddExampleController : Controller
    {
        //
        // GET: /AddExample/

        [HttpGet]
        public ActionResult AddExample()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddExample(AddExample obj)
        {
            obj.Calculate();

            return View(obj);
        }
    }
}
