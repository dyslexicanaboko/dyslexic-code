using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class OwnerController : Controller
    {
        public ActionResult Ownership()
        {
            List<Owner> lst = new List<Owner>();

            for (int i = 0; i <= 10; i++)
                lst.Add(new Owner(i));

            /* List the available owners
             * Perform ownership functions
             * Do error checks and display meaningful error messages
             */

            return View(lst);
        }
    }
}
