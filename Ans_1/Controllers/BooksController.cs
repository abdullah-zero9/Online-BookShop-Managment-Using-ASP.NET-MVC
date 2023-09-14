using Ans_1.Models;
using Ans_1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ans_1.Controllers
{
    public class BooksController : Controller
    {
        private readonly R53_MVCEntities db = new R53_MVCEntities();
        // GET: Books
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.categories = new SelectList(db.Categories, "CId", "CName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookVM tvm)
        {
            if (ModelState.IsValid)
            {
                if (tvm.Picture != null)
                {
                    //for Image
                    string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(tvm.Picture.FileName));
                    tvm.Picture.SaveAs(Server.MapPath(filePath));

                    //Books
                    Book books = new Book
                    {
                        BookName = tvm.BookName,
                        CId = tvm.CId,
                        Price = tvm.Price,
                        PublishDate = tvm.PublishDate,
                        Avialable = tvm.Available,
                        PicturePath = filePath
                    };
                    db.Books.Add(books);
                    db.SaveChanges();
                    return PartialView("_success");
                }
            }
            ViewBag.categories = new SelectList(db.Categories, "CId", "CName");
            return PartialView("_error");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Book books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            BookVM tvm = new BookVM
            {
                Id = books.Id,
                BookName = books.BookName,
                CId = books.CId,
                Price = books.Price,
                PublishDate = books.PublishDate,
                PicturePath = books.PicturePath,
                Available = books.Avialable
            };
            ViewBag.categories = new SelectList(db.Categories, "CId", "CName");
            return View(tvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookVM tvm)
        {
            if (ModelState.IsValid)
            {
                string filePath = tvm.PicturePath;
                if (tvm.Picture != null)
                {
                    //for Image
                    filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(tvm.Picture.FileName));
                    tvm.Picture.SaveAs(Server.MapPath(filePath));

                    Book books = new Book
                    {
                        Id = tvm.Id,
                        BookName = tvm.BookName,
                        CId = tvm.CId,
                        Price = tvm.Price,
                        PublishDate = tvm.PublishDate,
                        PicturePath = filePath,
                        Avialable = tvm.Available
                    };
                    db.Entry(books).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Book books = new Book
                    {
                        Id = tvm.Id,
                        BookName = tvm.BookName,
                        CId = tvm.CId,
                        Price = tvm.Price,
                        PublishDate = tvm.PublishDate,
                        PicturePath = filePath,
                        Avialable = tvm.Available
                    };
                    db.Entry(books).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.categories = new SelectList(db.Categories, "CId", "CName");
            return View(tvm);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Book books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Book books = db.Books.Find(id);
            string file_name = books.PicturePath;
            string path = Server.MapPath(file_name);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            db.Books.Remove(books);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}