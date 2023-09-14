using Ans_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ans_1.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly R53_MVCEntities db = new R53_MVCEntities();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category c)
        {
            if (string.IsNullOrEmpty(c.CName))
            {
                ModelState.AddModelError("CName", "value can't be empty");
            }
            if (ModelState.IsValid)
            {
                db.Categories.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c);
        }
    }
}