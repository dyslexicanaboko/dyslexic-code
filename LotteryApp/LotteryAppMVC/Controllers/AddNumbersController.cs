using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LotteryAppMVC.Controllers
{
    public class AddNumbersController : Controller
    {
        //
        // GET: /AddNumbers/

        public ActionResult Index()
        {
            return View();
        }

        public void Add() //int x, int y
        {
            //return View(new { X = x, Y = y});
            RedirectToAction("Index");
        }
    }
}
