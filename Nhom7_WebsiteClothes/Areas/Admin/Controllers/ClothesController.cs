using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom7_WebsiteClothes.Models;
using PagedList;

namespace Nhom7_WebsiteClothes.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ClothesController : Controller
    {
        private ClothesModelContext db = new ClothesModelContext();

        // GET: Admin/Clothes
        public ActionResult Index(int? page)
        {
            int pageIndex = page == null ? 1 : page.Value;
            int pageSize = 10;
            var context = new ClothesModelContext();
            var clothes = context.Clothes.Include(b => b.Category).ToList().ToPagedList(pageIndex, pageSize);
            return View(clothes);
        }

        public ActionResult Search(string searchString)
        {
            var context = new ClothesModelContext();
            var listClothesSearch = context.Clothes.Include(b => b.Category).Where(p => p.Title.Contains(searchString) || p.Description.Contains(searchString)).OrderBy(p => p.Title).ToPagedList(1, 4);
            return View("Index", listClothesSearch);
        }

        // GET: Admin/Clothes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloth cloth = db.Clothes.Find(id);
            if (cloth == null)
            {
                return HttpNotFound();
            }
            return View(cloth);
        }

        // GET: Admin/Clothes/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName");
            return View();
        }

        // POST: Admin/Clothes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,CategoryId,SizeId,Price,Image")] Cloth cloth, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/ImageClothes"), fileName);
                ImageFile.SaveAs(filePath);
                cloth.Image = "" + fileName;
            }
            if (ModelState.IsValid)
            {
                db.Clothes.Add(cloth);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", cloth.CategoryId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName", cloth.SizeId);
            return View(cloth);
        }

        // GET: Admin/Clothes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloth cloth = db.Clothes.Find(id);
            if (cloth == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", cloth.CategoryId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName", cloth.SizeId);
            return View(cloth);
        }

        // POST: Admin/Clothes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,CategoryId,SizeId,Price,Image")] Cloth cloth)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cloth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", cloth.CategoryId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName", cloth.SizeId);
            return View(cloth);
        }

        // GET: Admin/Clothes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloth cloth = db.Clothes.Find(id);
            if (cloth == null)
            {
                return HttpNotFound();
            }
            return View(cloth);
        }

        // POST: Admin/Clothes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cloth cloth = db.Clothes.Find(id);
            db.Clothes.Remove(cloth);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
